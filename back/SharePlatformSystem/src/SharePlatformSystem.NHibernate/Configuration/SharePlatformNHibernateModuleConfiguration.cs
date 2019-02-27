using FluentNHibernate.Cfg;

namespace SharePlatformSystem.NHibernate.Configuration
{
    internal class SharePlatformNHibernateModuleConfiguration :ISharePlatformNHibernateModuleConfiguration
    {
        public FluentConfiguration FluentConfiguration { get; }

        public SharePlatformNHibernateModuleConfiguration()
        {
            FluentConfiguration = Fluently.Configure();
        }
    }
}