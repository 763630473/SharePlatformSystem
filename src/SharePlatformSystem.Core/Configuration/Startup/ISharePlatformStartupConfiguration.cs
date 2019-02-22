using System;
using System.Collections.Generic;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// Used to configure ABP and modules on startup.
    /// </summary>
    public interface ISharePlatformStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// Gets the IOC manager associated with this configuration.
        /// </summary>
        IIocManager IocManager { get; }       

        Dictionary<string, object> GetCustomConfig();
    }
}