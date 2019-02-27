using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.Reflection.Extensions;

namespace SharePlatformSystem.HangFire
{
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformHangfireAspNetCoreModule : SharePlatformModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharePlatformHangfireAspNetCoreModule).GetAssembly());
        }
    }
}
