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
        /// 将SharePlatform集成到ASPNET核心。
        /// </summary>
        /// <typeparam name="TStartupModule">应用程序的启动模块，依赖于其他使用的模块。应源自<see cref=“shareplatformmodule”/>。</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="optionsAction">获取/修改选项的操作</param>
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

            //使用DI创建控制器
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //使用DI创建视图组件
            services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());


            //配置JSON序列化程序
            services.Configure<MvcJsonOptions>(jsonOptions =>
            {
                jsonOptions.SerializerSettings.ContractResolver = new SharePlatformMvcContractResolver(iocResolver)
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            //配置MVC
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