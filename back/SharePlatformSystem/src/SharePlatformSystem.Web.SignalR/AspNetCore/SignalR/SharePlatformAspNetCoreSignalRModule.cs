using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using System.Reflection;

namespace SharePlatform.AspNetCore.SignalR
{
    /// <summary>
    /// SharePlatform ASP.NET核心信号器集成模块。
    /// </summary>
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformAspNetCoreSignalRModule : SharePlatformModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
