using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// Used to configure background job system.
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// Used to enable/disable background job execution.
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }

        /// <summary>
        /// Gets the SharePlatform configuration object.
        /// </summary>
        ISharePlatformStartupConfiguration SharePlatformConfiguration { get; }
    }
}