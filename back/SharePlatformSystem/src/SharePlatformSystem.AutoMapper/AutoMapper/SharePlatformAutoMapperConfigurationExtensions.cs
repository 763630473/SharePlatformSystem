using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.AutoMapper
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure SharePlatform.AutoMapper module.
    /// </summary>
    public static class SharePlatformAutoMapperConfigurationExtensions
    {
        /// <summary>
        /// Used to configure SharePlatform.AutoMapper module.
        /// </summary>
        public static ISharePlatformAutoMapperConfiguration  SharePlatformAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformAutoMapperConfiguration> ();
        }
    }
}