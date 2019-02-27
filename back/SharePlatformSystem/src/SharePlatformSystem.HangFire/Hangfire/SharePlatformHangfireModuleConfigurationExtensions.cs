using SharePlatformSystem.BackgroundJobs;
using SharePlatformSystem.Core.Configuration.Startup;
namespace SharePlatformSystem.HangFire
{
    public static class SharePlatformHangfireConfigurationExtensions
    {
        /// <summary>
        /// Configures to use Hangfire for background job management.
        /// </summary>
        public static void UseHangfire(this IBackgroundJobConfiguration backgroundJobConfiguration)
        {
            backgroundJobConfiguration.SharePlatformConfiguration.ReplaceService<IBackgroundJobManager, HangfireBackgroundJobManager>();
        }
    }
}