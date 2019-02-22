using SharePlatformSystem.Dependency;
using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// This class is used to configure ABP and modules on startup.
    /// </summary>
    internal class SharePlatformStartupConfiguration : DictionaryBasedConfig, ISharePlatformStartupConfiguration
    {
        /// <summary>
        /// Reference to the IocManager.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
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
        /// Private constructor for singleton pattern.
        /// </summary>
        public SharePlatformStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Initialize()
        {
            //Localization = IocManager.Resolve<ILocalizationConfiguration>();
            //Modules = IocManager.Resolve<IModuleConfigurations>();
            //Features = IocManager.Resolve<IFeatureConfiguration>();
            //Navigation = IocManager.Resolve<INavigationConfiguration>();
            //Authorization = IocManager.Resolve<IAuthorizationConfiguration>();
            //Validation = IocManager.Resolve<IValidationConfiguration>();
            //Settings = IocManager.Resolve<ISettingsConfiguration>();
            //UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            //EventBus = IocManager.Resolve<IEventBusConfiguration>();
            //MultiTenancy = IocManager.Resolve<IMultiTenancyConfig>();
            //Auditing = IocManager.Resolve<IAuditingConfiguration>();
            //Caching = IocManager.Resolve<ICachingConfiguration>();
            //BackgroundJobs = IocManager.Resolve<IBackgroundJobConfiguration>();
            //Notifications = IocManager.Resolve<INotificationConfiguration>();
            //EmbeddedResources = IocManager.Resolve<IEmbeddedResourcesConfiguration>();
            //EntityHistory = IocManager.Resolve<IEntityHistoryConfiguration>();

            //CustomConfigProviders = new List<ICustomConfigProvider>();
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
    }
}