using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.Quartz.Configuration
{
    public static class SharePlatformQuartzConfigurationExtensions
    {
        /// <summary>
        ///     Used to configure ABP Quartz module.
        /// </summary>
        public static ISharePlatformQuartzConfiguration SharePlatformQuartz(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformQuartzConfiguration>();
        }
    }
}
