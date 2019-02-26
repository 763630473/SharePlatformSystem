using System.Reflection;

namespace AbpAspNetCoreDemo.PlugIn
{
    [DependsOn(typeof(AbpAspNetCoreModule))]
    public class SharePlatformAspNetCoreDemoPlugInModule : AbpModule
    {
        public override void PreInitialize()
        {

            Configuration.EmbeddedResources.Sources.Add(
                new EmbeddedResourceSet(
                    "/Views/",
                    typeof(SharePlatformAspNetCoreDemoPlugInModule).GetAssembly(),
                    "AbpAspNetCoreDemo.PlugIn.Views"
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharePlatformAspNetCoreDemoPlugInModule).GetAssembly());
        }
    }
}
