using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// 用于配置后台作业系统。
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// 用于启用/禁用后台作业执行。
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }

        /// <summary>
        /// 获取SharePlatform配置对象。
        /// </summary>
        ISharePlatformStartupConfiguration SharePlatformConfiguration { get; }
    }
}