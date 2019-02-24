using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// Used to provide a way to configure modules.
    /// Create entension methods to this class to be used over <see cref="ISharePlatformStartupConfiguration.Modules"/> object.
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the SharePlatform configuration object.
        /// </summary>
        ISharePlatformStartupConfiguration SharePlatformConfiguration { get; }
    }
}