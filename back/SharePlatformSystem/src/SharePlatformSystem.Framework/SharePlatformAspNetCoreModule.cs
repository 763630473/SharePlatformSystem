using System.Linq;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.Reflection.Extensions;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Framework.AspNetCore.Configuration;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Auditing;
using SharePlatformSystem.Framework.AspNetCore.Runtime.Session;
using SharePlatformSystem.Runtime.Session;
using SharePlatformSystem.Core.Configuration.Startup;
using SharePlatformSystem.Framework.Api.ProxyScripting.Configuration;

namespace SharePlatformSystem.Framework.AspNetCore
{
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformAspNetCoreModule : SharePlatformModule
    {
        public override void PreInitialize()
        {

            IocManager.AddConventionalRegistrar(new SharePlatformAspNetCoreConventionalRegistrar());

            IocManager.Register<ISharePlatformAspNetCoreConfiguration, SharePlatformAspNetCoreConfiguration>();
            IocManager.Register<ISharePlatformWebCommonModuleConfiguration, SharePlatformWebCommonModuleConfiguration>();

            IocManager.Register<IApiProxyScriptingConfiguration, ApiProxyScriptingConfiguration>();
            IocManager.Register<IWebEmbeddedResourcesConfiguration, WebEmbeddedResourcesConfiguration>();
            
            Configuration.ReplaceService<IPrincipalAccessor, AspNetCorePrincipalAccessor>(DependencyLifeStyle.Transient);
            Configuration.ReplaceService<IClientInfoProvider, HttpContextClientInfoProvider>(DependencyLifeStyle.Transient);

            Configuration.Modules.SharePlatformAspNetCore().FormBodyBindingIgnoredTypes.Add(typeof(IFormFile));

          
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharePlatformAspNetCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            AddApplicationParts();
        }

        private void AddApplicationParts()
        {
            var configuration = IocManager.Resolve<SharePlatformAspNetCoreConfiguration>();
            var partManager = IocManager.Resolve<ApplicationPartManager>();
            var moduleManager = IocManager.Resolve<ISharePlatformModuleManager>();

            var controllerAssemblies = configuration.ControllerAssemblySettings.Select(s => s.Assembly).Distinct();
            foreach (var controllerAssembly in controllerAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(controllerAssembly));
            }

            var plugInAssemblies = moduleManager.Modules.Where(m => m.IsLoadedAsPlugIn).Select(m => m.Assembly).Distinct();
            foreach (var plugInAssembly in plugInAssemblies)
            {
                partManager.ApplicationParts.Add(new AssemblyPart(plugInAssembly));
            }
        }    
    }
}