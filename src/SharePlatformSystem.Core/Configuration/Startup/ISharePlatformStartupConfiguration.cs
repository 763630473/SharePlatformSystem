using System;
using System.Collections.Generic;
using SharePlatformSystem.BackgroundJobs;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;

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
    }
}