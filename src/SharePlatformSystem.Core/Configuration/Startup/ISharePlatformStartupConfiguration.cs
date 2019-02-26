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
    /// Used to configure SharePlatform and modules on startup.
    /// </summary>
    public interface ISharePlatformStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// Gets the IOC manager associated with this configuration.
        /// </summary>
        IIocManager IocManager { get; }       

        Dictionary<string, object> GetCustomConfig();
        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }
        /// <summary>
        /// Used to replace a service type.
        /// Given <see cref="replaceAction"/> should register an implementation for the <see cref="type"/>.
        /// </summary>
        /// <param name="type">The type to be replaced.</param>
        /// <param name="replaceAction">Replace action.</param>
        void ReplaceService(Type type, Action replaceAction);
        /// <summary>
        /// Used to configure unit of work defaults.
        /// </summary>
        IUnitOfWorkDefaultOptions UnitOfWork { get; }

        /// <summary>
        /// Gets a configuration object.
        /// </summary>
        T Get<T>();
        /// <summary>
        /// Used to configure modules.
        /// Modules can write extension methods to <see cref="IModuleConfigurations"/> to add module specific configurations.
        /// </summary>
        IModuleConfigurations Modules { get; }
        /// <summary>
        /// Used to configure background job system.
        /// </summary>
        IBackgroundJobConfiguration BackgroundJobs { get; }
        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// </summary>
        IEventBusConfiguration EventBus { get; }
        /// <summary>
        /// Used to configure settings.
        /// </summary>
        ISettingsConfiguration Settings { get; }
        /// <summary>
        /// Used to configure entity history.
        /// </summary>
        IEntityHistoryConfiguration EntityHistory { get; }
        /// <summary>
        /// Used to configure auditing.
        /// </summary>
        IAuditingConfiguration Auditing { get; }

        /// <summary>
        /// Used to configure caching.
        /// </summary>
        ICachingConfiguration Caching { get; }
        /// <summary>
        /// Used to set localization configuration.
        /// </summary>
        ILocalizationConfiguration Localization { get; }
        IEmbeddedResourcesConfiguration EmbeddedResources { get; }
    }
}