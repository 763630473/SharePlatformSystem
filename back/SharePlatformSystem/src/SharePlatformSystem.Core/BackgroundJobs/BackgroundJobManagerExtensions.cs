using SharePlatformSystem.Threading;
using System;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// “IBackgroundJobManager”的一些扩展方法。
    /// </summary>
    public static class BackgroundJobManagerExtensions
    {
        /// <summary>
        ///将要执行的作业排队。
        /// </summary>
        /// <typeparam name="TJob">作业的类型。</typeparam>
        /// <typeparam name="TArgs">作业参数的类型。</typeparam>
        /// <param name="backgroundJobManager">后台作业管理器参考</param>
        /// <param name="args">工作参数。</param>
        /// <param name="priority">工作优先级。</param>
        /// <param name="delay">作业延迟（第一次尝试前的等待时间）。</param>
        public static void Enqueue<TJob, TArgs>(this IBackgroundJobManager backgroundJobManager, TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>
        {
            AsyncHelper.RunSync(() => backgroundJobManager.EnqueueAsync<TJob, TArgs>(args, priority, delay));
        }
    }
}
