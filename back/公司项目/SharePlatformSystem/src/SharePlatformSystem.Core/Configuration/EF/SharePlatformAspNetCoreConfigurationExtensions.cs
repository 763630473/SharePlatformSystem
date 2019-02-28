

using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP EntityFramework Core module.
    /// </summary>
    public static class SharePlatformEfCoreConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP EntityFramework Core module.
        /// </summary>
        public static ISharePlatformEfCoreConfiguration SharePlatformEfCore(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformEfCoreConfiguration>();
        }
    }
}