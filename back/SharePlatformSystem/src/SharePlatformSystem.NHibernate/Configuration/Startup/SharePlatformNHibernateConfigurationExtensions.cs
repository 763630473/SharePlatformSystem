using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.NHibernate.Configuration.Startup
{
    /// <summary>
    ///将扩展方法定义为允许配置SharePlatform nhibernate模块。
    /// </summary>
    public static class SharePlatformNHibernateConfigurationExtensions
    {
        /// <summary>
        /// 用于配置SharePlatform NHibernate模块。
        /// </summary>
        public static ISharePlatformNHibernateModuleConfiguration SharePlatformNHibernate(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformNHibernateModuleConfiguration>();
        }
    }
}