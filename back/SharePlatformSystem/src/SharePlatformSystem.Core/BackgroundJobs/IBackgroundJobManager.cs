using SharePlatformSystem.Threading.BackgroundWorkers;
using System;
using System.Threading.Tasks;
namespace SharePlatformSystem.BackgroundJobs
{
    //TODO: 为IBackgroundJobManager创建非泛型EnqueueAsync扩展方法，该方法将类型作为输入参数，而不是泛型参数。
    /// <summary>
    /// 定义作业管理器的接口。
    /// </summary>
    public interface IBackgroundJobManager : IBackgroundWorker
    {
        /// <summary>
        /// 将要执行的作业排队。
        /// </summary>
        /// <typeparam name="TJob">作业的类型。</typeparam>
        /// <typeparam name="TArgs">作业参数的类型。</typeparam>
        /// <param name="args">工作参数。</param>
        /// <param name="priority">工作优先级。</param>
        /// <param name="delay">作业延迟（第一次尝试前的等待时间）。</param>
        /// <returns>后台作业的唯一标识符。</returns>
        Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>;

        /// <summary>
        /// 删除具有指定jobid的作业。
        /// </summary>
        /// <param name="jobId">作业唯一标识符。</param>
        /// <returns><c>True</c> 在成功的完全状态转换时，<c>false<c>否则。</returns>
        Task<bool> DeleteAsync(string jobId);
    }
}