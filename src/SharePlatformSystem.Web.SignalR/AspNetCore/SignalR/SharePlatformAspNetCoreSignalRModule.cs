using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using System.Reflection;

namespace SharePlatform.AspNetCore.SignalR
{
    /// <summary>
    /// ABP ASP.NET Core SignalR integration module.
    /// </summary>
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformAspNetCoreSignalRModule : SharePlatformModule
    {
        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
