using System;
using System.Collections.Generic;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharePlatformSystem.Framework.AspNetCore.Configuration;
using SharePlatformSystem.Applications.Services;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Reflection;
using SharePlatformSystem.Framework.Api.ProxyScripting.Generators;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Conventions
{
    public class SharePlatformAppServiceConvention : IApplicationModelConvention
    {
        private readonly Lazy<SharePlatformAspNetCoreConfiguration> _configuration;

        public SharePlatformAppServiceConvention(IServiceCollection services)
        {
            _configuration = new Lazy<SharePlatformAspNetCoreConfiguration>(() =>
            {
                return services
                    .GetSingletonService<SharePlatformBootstrapper>()
                    .IocManager
                    .Resolve<SharePlatformAspNetCoreConfiguration>();
            }, true);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var type = controller.ControllerType.AsType();
                var configuration = GetControllerSettingOrNull(type);

                if (typeof(IApplicationService).GetTypeInfo().IsAssignableFrom(type))
                {
                    controller.ControllerName = controller.ControllerName.RemovePostFix(ApplicationService.CommonPostfixes);
                    configuration?.ControllerModelConfigurer(controller);

                    ConfigureArea(controller, configuration);
                    ConfigureRemoteService(controller, configuration);
                }
                else
                {
                    var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(type.GetTypeInfo());
                    if (remoteServiceAtt != null && remoteServiceAtt.IsEnabledFor(type))
                    {
                        ConfigureRemoteService(controller, configuration);
                    }
                }
            }
        }

        private void ConfigureArea(ControllerModel controller, [CanBeNull] SharePlatformControllerAssemblySetting configuration)
        {
            if (configuration == null)
            {
                return;
            }

            if (controller.RouteValues.ContainsKey("area"))
            {
                return;
            }

            controller.RouteValues["area"] = configuration.ModuleName;
        }

        private void ConfigureRemoteService(ControllerModel controller, [CanBeNull] SharePlatformControllerAssemblySetting configuration)
        {
            ConfigureApiExplorer(controller);
            ConfigureSelector(controller, configuration);
            ConfigureParameters(controller);
        }

        private void ConfigureParameters(ControllerModel controller)
        {
            foreach (var action in controller.Actions)
            {
                foreach (var prm in action.Parameters)
                {
                    if (prm.BindingInfo != null)
                    {
                        continue;
                    }

                    if (!TypeHelper.IsPrimitiveExtendedIncludingNullable(prm.ParameterInfo.ParameterType))
                    {
                        if (CanUseFormBodyBinding(action, prm))
                        {
                            prm.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                        }
                    }
                }
            }
        }

        private bool CanUseFormBodyBinding(ActionModel action, ParameterModel parameter)
        {
            if (_configuration.Value.FormBodyBindingIgnoredTypes.Any(t => t.IsAssignableFrom(parameter.ParameterInfo.ParameterType)))
            {
                return false;
            }

            foreach (var selector in action.Selectors)
            {
                if (selector.ActionConstraints == null)
                {
                    continue;
                }

                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    var httpMethodActionConstraint = actionConstraint as HttpMethodActionConstraint;
                    if (httpMethodActionConstraint == null)
                    {
                        continue;
                    }

                    if (httpMethodActionConstraint.HttpMethods.All(hm => hm.IsIn("GET", "DELETE", "TRACE", "HEAD")))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ConfigureApiExplorer(ControllerModel controller)
        {
            if (controller.ApiExplorer.GroupName.IsNullOrEmpty())
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }

            if (controller.ApiExplorer.IsVisible == null)
            {
                var controllerType = controller.ControllerType.AsType();
                var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(controllerType.GetTypeInfo());
                if (remoteServiceAtt != null)
                {
                    controller.ApiExplorer.IsVisible =
                        remoteServiceAtt.IsEnabledFor(controllerType) &&
                        remoteServiceAtt.IsMetadataEnabledFor(controllerType);
                }
                else
                {
                    controller.ApiExplorer.IsVisible = true;
                }
            }

            foreach (var action in controller.Actions)
            {
                ConfigureApiExplorer(action);
            }
        }

        private void ConfigureApiExplorer(ActionModel action)
        {
            if (action.ApiExplorer.IsVisible == null)
            {
                var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(action.ActionMethod);
                if (remoteServiceAtt != null)
                {
                    action.ApiExplorer.IsVisible =
                        remoteServiceAtt.IsEnabledFor(action.ActionMethod) &&
                        remoteServiceAtt.IsMetadataEnabledFor(action.ActionMethod);
                }
            }
        }

        private void ConfigureSelector(ControllerModel controller, [CanBeNull] SharePlatformControllerAssemblySetting configuration)
        {
            RemoveEmptySelectors(controller.Selectors);

            if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
            {
                return;
            }

            var moduleName = GetModuleNameOrDefault(controller.ControllerType.AsType());

            foreach (var action in controller.Actions)
            {
                ConfigureSelector(moduleName, controller.ControllerName, action, configuration);
            }
        }

        private void ConfigureSelector(string moduleName, string controllerName, ActionModel action, [CanBeNull] SharePlatformControllerAssemblySetting configuration)
        {
            RemoveEmptySelectors(action.Selectors);

            var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(action.ActionMethod);
            if (remoteServiceAtt != null && !remoteServiceAtt.IsEnabledFor(action.ActionMethod))
            {
                return;
            }

            if (!action.Selectors.Any())
            {
                AddSharePlatformServiceSelector(moduleName, controllerName, action, configuration);
            }
            else
            {
                NormalizeSelectorRoutes(moduleName, controllerName, action);
            }
        }

        private void AddSharePlatformServiceSelector(string moduleName, string controllerName, ActionModel action, [CanBeNull] SharePlatformControllerAssemblySetting configuration)
        {
            var SharePlatformServiceSelectorModel = new SelectorModel
            {
                AttributeRouteModel = CreateSharePlatformServiceAttributeRouteModel(moduleName, controllerName, action)
            };

            var verb = configuration?.UseConventionalHttpVerbs == true
                           ? ProxyScriptingHelper.GetConventionalVerbForMethodName(action.ActionName)
                           : ProxyScriptingHelper.DefaultHttpVerb;

            SharePlatformServiceSelectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { verb }));

            action.Selectors.Add(SharePlatformServiceSelectorModel);
        }

        private static void NormalizeSelectorRoutes(string moduleName, string controllerName, ActionModel action)
        {
            foreach (var selector in action.Selectors)
            {
                if (selector.AttributeRouteModel == null)
                {
                    selector.AttributeRouteModel = CreateSharePlatformServiceAttributeRouteModel(
                        moduleName,
                        controllerName,
                        action
                    );
                }
            }
        }

        private string GetModuleNameOrDefault(Type controllerType)
        {
            return GetControllerSettingOrNull(controllerType)?.ModuleName ??
                   SharePlatformControllerAssemblySetting.DefaultServiceModuleName;
        }

        [CanBeNull]
        private SharePlatformControllerAssemblySetting GetControllerSettingOrNull(Type controllerType)
        {
            var settings = _configuration.Value.ControllerAssemblySettings.GetSettings(controllerType);
            return settings.FirstOrDefault(setting => setting.TypePredicate(controllerType));
        }

        private static AttributeRouteModel CreateSharePlatformServiceAttributeRouteModel(string moduleName, string controllerName, ActionModel action)
        {
            return new AttributeRouteModel(
                new RouteAttribute(
                    $"api/services/{moduleName}/{controllerName}/{action.ActionName}"
                )
            );
        }

        private static void RemoveEmptySelectors(IList<SelectorModel> selectors)
        {
            selectors
                .Where(IsEmptySelector)
                .ToList()
                .ForEach(s => selectors.Remove(s));
        }

        private static bool IsEmptySelector(SelectorModel selector)
        {
            return selector.AttributeRouteModel == null && selector.ActionConstraints.IsNullOrEmpty();
        }
    }
}