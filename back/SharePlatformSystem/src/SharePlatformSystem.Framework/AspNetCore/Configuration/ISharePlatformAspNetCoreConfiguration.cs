using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Caching;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Framework.Models;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    public interface ISharePlatformAspNetCoreConfiguration
    {
        WrapResultAttribute DefaultWrapResultAttribute { get; }

        IClientCacheAttribute DefaultClientCacheAttribute { get; set; }

        UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        List<Type> FormBodyBindingIgnoredTypes { get; }

        /// <summary>
        /// 默认值: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }

        /// <summary>
        /// 用于启用/禁用MVC控制器的审核。
        /// 默认值: true.
        /// </summary>
        bool IsAuditingEnabled { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool SetNoCacheForAjaxResponses { get; set; }

        /// <summary>
        /// 默认值: false.
        /// </summary>
        bool UseMvcDateTimeFormatForAppServices { get; set; }

        /// <summary>
        /// 用于为模块添加路由配置。
        /// </summary>
        List<Action<IRouteBuilder>> RouteConfiguration { get; }

        SharePlatformControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = SharePlatformControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true
        );
    }
}
