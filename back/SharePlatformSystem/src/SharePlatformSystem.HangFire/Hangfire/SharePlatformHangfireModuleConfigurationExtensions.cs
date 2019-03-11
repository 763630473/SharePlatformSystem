using SharePlatformSystem.BackgroundJobs;
using SharePlatformSystem.Core.Configuration.Startup;
namespace SharePlatformSystem.HangFire
{
    public static class SharePlatformHangfireConfigurationExtensions
    {
        /// <summary>
        /// 配置为使用hangfire进行后台作业管理。
        /// </summary>
        public static void UseHangfire(this IBackgroundJobConfiguration backgroundJobConfiguration)
        {
            backgroundJobConfiguration.SharePlatformConfiguration.ReplaceService<IBackgroundJobManager, HangfireBackgroundJobManager>();
        }
    }
}