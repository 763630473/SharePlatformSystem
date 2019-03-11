using System;
using System.Collections.Generic;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.BackgroundJobs;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Resources.Embedded;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.EntityHistory;
using SharePlatformSystem.Runtime.Caching.Configuration;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    ///用于在启动时配置SharePlatform和模块。
    /// </summary>
    public interface ISharePlatformStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// 获取与此配置关联的IOC管理器。
        /// </summary>
        IIocManager IocManager { get; }       

        Dictionary<string, object> GetCustomConfig();
        /// <summary>
        ///获取/设置ORM模块使用的默认连接字符串。
        ///可以是应用程序配置文件中连接字符串的名称，也可以是完整连接字符串。
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }
        /// <summary>
        ///用于替换服务类型。
        ///给定的“replaceAction”应该为“type”注册一个实现。
        /// </summary>
        /// <param name="type">要替换的类型。</param>
        /// <param name="replaceAction">替换动作。</param>
        void ReplaceService(Type type, Action replaceAction);
        /// <summary>
        ///用于配置工作单位默认值。
        /// </summary>
        IUnitOfWorkDefaultOptions UnitOfWork { get; }

        /// <summary>
        /// 获取配置对象。
        /// </summary>
        T Get<T>();
        /// <summary>
        ///用于配置模块。
        ///模块可以将扩展方法写入“imoduleconfigurations”以添加模块特定的配置。
        /// </summary>
        IModuleConfigurations Modules { get; }
        /// <summary>
        /// 用于配置后台作业系统。
        /// </summary>
        IBackgroundJobConfiguration BackgroundJobs { get; }
        /// <summary>
        /// 用于配置“IEventBus”。
        /// </summary>
        IEventBusConfiguration EventBus { get; }
        /// <summary>
        /// 用于配置设置。
        /// </summary>
        ISettingsConfiguration Settings { get; }
        /// <summary>
        ///用于配置实体历史记录。
        /// </summary>
        IEntityHistoryConfiguration EntityHistory { get; }
        /// <summary>
        /// 用于配置审核。
        /// </summary>
        IAuditingConfiguration Auditing { get; }

        /// <summary>
        ///用于配置缓存。
        /// </summary>
        ICachingConfiguration Caching { get; }
        /// <summary>
        ///用于设置本地化配置。
        /// </summary>
        ILocalizationConfiguration Localization { get; }
        IEmbeddedResourcesConfiguration EmbeddedResources { get; }
    }
}