
using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    /// <summary>
    /// 将扩展方法定义为允许配置SharePlatform ASP.NET核心模块。
    /// </summary>
    public static class SharePlatformAspNetCoreConfigurationExtensions
    {
        /// <summary>
        /// 用于配置SharePlatform ASP.NET核心模块。
        /// </summary>
        public static ISharePlatformAspNetCoreConfiguration SharePlatformAspNetCore(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformAspNetCoreConfiguration>();
        }
    }
}