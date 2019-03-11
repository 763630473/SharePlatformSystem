using FluentNHibernate.Cfg;

namespace SharePlatformSystem.NHibernate.Configuration
{
    /// <summary>
    ///用于配置SharePlatform NHibernate模块。
    /// </summary>
    public interface ISharePlatformNHibernateModuleConfiguration
    {
        /// <summary>
        ///用于获取和修改nhibernate fluent配置。
        ///可以向此对象添加映射。
        ///不要对其调用buildsessionFactory。
        /// </summary>
        FluentConfiguration FluentConfiguration { get; }
    }
}