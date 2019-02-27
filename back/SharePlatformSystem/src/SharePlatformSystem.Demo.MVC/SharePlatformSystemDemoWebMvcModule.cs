using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.Reflection.Extensions;
using SharePlatformSystem.Core.Resources.Embedded;
using SharePlatformSystem.Framework.AspNetCore;
using System.Reflection;

namespace SharePlatformSystem.Demo.MVC.PlugIn
{
    [DependsOn(typeof(SharePlatformAspNetCoreModule))]
    public class SharePlatformSystemDemoWebMvcModule : SharePlatformModule
    {
        public override void PreInitialize()
        {

            var e = Configuration;
            var s = e.EmbeddedResources;
			s.Sources
				.Add(
                new EmbeddedResourceSet(
                    "/Views/",
                    typeof(SharePlatformSystemDemoWebMvcModule).GetAssembly(),
                    "SharePlatformSystem.Demo.MVC.Views"
                )
            );
       
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharePlatformSystemDemoWebMvcModule).GetAssembly());
        }
    }
}
