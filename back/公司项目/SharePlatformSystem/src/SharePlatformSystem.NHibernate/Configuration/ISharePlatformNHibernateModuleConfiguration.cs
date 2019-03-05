using FluentNHibernate.Cfg;

namespace SharePlatformSystem.NHibernate.Configuration
{
    /// <summary>
    /// Used to configure ABP NHibernate module.
    /// </summary>
    public interface ISharePlatformNHibernateModuleConfiguration
    {
        /// <summary>
        /// Used to get and modify NHibernate fluent configuration.
        /// You can add mappings to this object.
        /// Do not call BuildSessionFactory on it.
        /// </summary>
        FluentConfiguration FluentConfiguration { get; }
    }
}