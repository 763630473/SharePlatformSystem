using System;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Framework.Json;
using SharePlatformSystem.Framework.AspNetCore.EmbeddedResources;
using SharePlatformSystem.Framework.AspNetCore.Mvc;

namespace SharePlatformSystem.Framework.AspNetCore
{
    public static class SharePlatformServiceCollectionExtensions
    {
        /// <summary>
        /// Integrates SharePlatform to AspNet Core.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="SharePlatformModule"/>.</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="optionsAction">An action to get/modify options</param>
        public static IServiceProvider AddSharePlatform<TStartupModule>(this IServiceCollection services, [CanBeNull] Action<SharePlatformBootstrapperOptions> optionsAction = null)
            where TStartupModule : SharePlatformModule
        {
            var SharePlatformBootstrapper = AddSharePlatformBootstrapper<TStartupModule>(services, optionsAction);

            ConfigureAspNetCore(services, SharePlatformBootstrapper.IocManager);

            return WindsorRegistrationHelper.CreateServiceProvider(SharePlatformBootstrapper.IocManager.IocContainer, services);
        }

        private static void ConfigureAspNetCore(IServiceCollection services, IIocResolver iocResolver)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            
            //Use DI to create controllers
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //Use DI to create view components
            services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());
       

            //Configure JSON serializer
            services.Configure<MvcJsonOptions>(jsonOptions =>
            {
                jsonOptions.SerializerSettings.ContractResolver = new SharePlatformMvcContractResolver(iocResolver)
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            //Configure MVC
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddSharePlatform(services);
            });

            //Configure Razor
            services.Insert(0,
                ServiceDescriptor.Singleton<IConfigureOptions<RazorViewEngineOptions>>(
                    new ConfigureOptions<RazorViewEngineOptions>(
                        (options) =>
                        {
                            options.FileProviders.Add(new EmbeddedResourceViewFileProvider(iocResolver));
                        }
                    )
                )
            );
        }

        private static SharePlatformBootstrapper AddSharePlatformBootstrapper<TStartupModule>(IServiceCollection services, Action<SharePlatformBootstrapperOptions> optionsAction)
            where TStartupModule : SharePlatformModule
        {
            var sharePlatformBootstrapper = SharePlatformBootstrapper.Create<TStartupModule>(optionsAction);
            services.AddSingleton(sharePlatformBootstrapper);
            return sharePlatformBootstrapper;
        }
    }
}