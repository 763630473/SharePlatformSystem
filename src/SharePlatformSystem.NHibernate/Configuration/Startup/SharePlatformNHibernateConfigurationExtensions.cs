using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.NHibernate.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ABP NHibernate module.
    /// </summary>
    public static class SharePlatformNHibernateConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP NHibernate module.
        /// </summary>
        public static ISharePlatformNHibernateModuleConfiguration SharePlatformNHibernate(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformNHibernateModuleConfiguration>();
        }
    }
}