using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.Reflection.Extensions;

namespace SharePlatformSystem.TestBase
{
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformTestBaseModule : SharePlatformModule
    {
        public override void PreInitialize()
        {
            Configuration.EventBus.UseDefaultEventBus = false;
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharePlatformTestBaseModule).GetAssembly());
        }
    }
}