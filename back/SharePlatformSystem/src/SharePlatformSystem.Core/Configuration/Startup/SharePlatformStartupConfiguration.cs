using SharePlatformSystem.Auditing;
using SharePlatformSystem.BackgroundJobs;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Core.Resources.Embedded;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.EntityHistory;
using SharePlatformSystem.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 此类用于在启动时配置SharePlatform和模块。
    /// </summary>
    internal class SharePlatformStartupConfiguration : DictionaryBasedConfig, ISharePlatformStartupConfiguration
    {
        /// <summary>
        ///对IOCManager的引用。
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// 获取/设置ORM模块使用的默认连接字符串。
        ///可以是应用程序配置文件中连接字符串的名称，也可以是完整连接字符串。
        /// </summary>
        public string DefaultNameOrConnectionString { get; set; }

        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }

        public IList<ICustomConfigProvider> CustomConfigProviders { get; private set; }

        public Dictionary<string, object> GetCustomConfig()
        {
            var mergedConfig = new Dictionary<string, object>();

            using (var scope = IocManager.CreateScope())
            {
                foreach (var provider in CustomConfigProviders)
                {
                    var config = provider.GetConfig(new CustomConfigProviderContext(scope));
                    foreach (var keyValue in config)
                    {
                        mergedConfig[keyValue.Key] = keyValue.Value;
                    }
                }
            }

            return mergedConfig;
        }

        /// <summary>
        /// 单例模式的私有构造函数。
        /// </summary>
        public SharePlatformStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Initialize()
        {
            Localization = IocManager.Resolve<ILocalizationConfiguration>();
            Modules = IocManager.Resolve<IModuleConfigurations>();
            Settings = IocManager.Resolve<ISettingsConfiguration>();
            UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            EventBus = IocManager.Resolve<IEventBusConfiguration>();
            Auditing = IocManager.Resolve<IAuditingConfiguration>();
            Caching = IocManager.Resolve<ICachingConfiguration>();
            BackgroundJobs = IocManager.Resolve<IBackgroundJobConfiguration>();
            EntityHistory = IocManager.Resolve<IEntityHistoryConfiguration>();

            CustomConfigProviders = new List<ICustomConfigProvider>();
            ServiceReplaceActions = new Dictionary<Type, Action>();
        }

        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }
        /// <summary>
        /// 用于配置工作单位默认值。
        /// </summary>
        public IUnitOfWorkDefaultOptions UnitOfWork { get; private set; }
        /// <summary>
        ///用于配置模块。
        ///模块可以将扩展方法写入“module configurations”以添加模块特定的配置。
        /// </summary>
        public IModuleConfigurations Modules { get; private set; }
        /// <summary>
        /// 用于配置后台作业系统。
        /// </summary>
        public IBackgroundJobConfiguration BackgroundJobs { get; private set; }
        /// <summary>
        /// 用于配置“IEventBus”。
        /// </summary>
        public IEventBusConfiguration EventBus { get; private set; }
        /// <summary>
        ///用于配置设置。
        /// </summary>
        public ISettingsConfiguration Settings { get; private set; }
        /// <summary>
        /// 用于配置审核。
        /// </summary>
        public IAuditingConfiguration Auditing { get; private set; }

        public ICachingConfiguration Caching { get; private set; }
        public IEntityHistoryConfiguration EntityHistory { get; private set; }

        /// <summary>
        /// 用于设置本地化配置。
        /// </summary>
        public ILocalizationConfiguration Localization { get; private set; }
        public IEmbeddedResourcesConfiguration EmbeddedResources { get; private set; }
    }
}