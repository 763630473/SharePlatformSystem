using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.BackgroundJobs
{
    internal class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        public bool IsJobExecutionEnabled { get; set; }
        
        public ISharePlatformStartupConfiguration SharePlatformConfiguration { get; private set; }

        public BackgroundJobConfiguration(ISharePlatformStartupConfiguration abpConfiguration)
        {
            SharePlatformConfiguration = abpConfiguration;

            IsJobExecutionEnabled = true;
        }
    }
}