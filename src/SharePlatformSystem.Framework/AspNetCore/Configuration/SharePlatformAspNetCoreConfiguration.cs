using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Results.Caching;
using SharePlatformSystem.Framework.Models;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    public class SharePlatformAspNetCoreConfiguration : ISharePlatformAspNetCoreConfiguration
    {
        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public IClientCacheAttribute DefaultClientCacheAttribute { get; set; }

        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public List<Type> FormBodyBindingIgnoredTypes { get; }

        public ControllerAssemblySettingList ControllerAssemblySettings { get; }

        public bool IsValidationEnabledForControllers { get; set; }

        public bool IsAuditingEnabled { get; set; }

        public bool SetNoCacheForAjaxResponses { get; set; }

        public bool UseMvcDateTimeFormatForAppServices { get; set; }

        public List<Action<IRouteBuilder>> RouteConfiguration { get; }

        public SharePlatformAspNetCoreConfiguration()
        {
            DefaultWrapResultAttribute = new WrapResultAttribute();
            DefaultClientCacheAttribute = new NoClientCacheAttribute(false);
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            ControllerAssemblySettings = new ControllerAssemblySettingList();
            FormBodyBindingIgnoredTypes = new List<Type>();
            RouteConfiguration = new List<Action<IRouteBuilder>>();
            IsValidationEnabledForControllers = true;
            SetNoCacheForAjaxResponses = true;
            IsAuditingEnabled = true;
            UseMvcDateTimeFormatForAppServices = false;
        }
       
        public SharePlatformControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = SharePlatformControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true)
        {
            var setting = new SharePlatformControllerAssemblySetting(moduleName, assembly, useConventionalHttpVerbs);
            ControllerAssemblySettings.Add(setting);
            return new SharePlatformControllerAssemblySettingBuilder(setting);
        }
    }
}