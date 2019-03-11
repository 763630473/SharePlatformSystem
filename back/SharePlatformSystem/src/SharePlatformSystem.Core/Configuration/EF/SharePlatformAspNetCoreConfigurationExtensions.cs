

using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 将扩展方法定义为“IModuleConfigurations”，以允许配置ABP EntityFramework核心模块。
    /// </summary>
    public static class SharePlatformEfCoreConfigurationExtensions
    {
        /// <summary>
        /// 用于配置 entityframework核心模块。
        /// </summary>
        public static ISharePlatformEfCoreConfiguration SharePlatformEfCore(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformEfCoreConfiguration>();
        }
    }
}