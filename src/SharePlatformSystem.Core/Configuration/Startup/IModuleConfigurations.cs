using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// Used to provide a way to configure modules.
    /// Create entension methods to this class to be used over <see cref="IAbpStartupConfiguration.Modules"/> object.
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the ABP configuration object.
        /// </summary>
        ISharePlatformStartupConfiguration SharePlatformConfiguration { get; }
    }
}