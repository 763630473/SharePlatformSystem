
using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure SharePlatform ASP.NET Core module.
    /// </summary>
    public static class SharePlatformAspNetCoreConfigurationExtensions
    {
        /// <summary>
        /// Used to configure SharePlatform ASP.NET Core module.
        /// </summary>
        public static ISharePlatformAspNetCoreConfiguration SharePlatformAspNetCore(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformAspNetCoreConfiguration>();
        }
    }
}