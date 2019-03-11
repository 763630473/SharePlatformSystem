using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// 定义用于存储/获取后台作业的接口。
    /// </summary>
    public interface IBackgroundJobStore
    {
        /// <summary>
        /// 基于给定的JobID获取BackgroundJobInfo。
        /// </summary>
        /// <param name="jobId">作业唯一标识符。</param>
        /// <returns>BackgroundJobInfo对象</returns>
        Task<BackgroundJobInfo> GetAsync(string jobId);

        /// <summary>
        ///插入后台作业。
        /// </summary>
        /// <param name="jobInfo">工作信息。</param>
        Task InsertAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        ///获取等待的作业。它应该根据以下条件获得工作：
        ///条件：！isabandoned和nexttrytime&lt；=clock.now。
        ///排序依据：priority desc，trycount asc，nexttrytime asc。
        ///最大结果：“maxResultCount”。
        /// </summary>
        /// <param name="maxResultCount">最大结果计数。</param>
        Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount);

        /// <summary>
        /// 删除作业。
        /// </summary>
        /// <param name="jobInfo">Job information.</param>
        Task DeleteAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// 更新作业。
        /// </summary>
        /// <param name="jobInfo">工作信息。</param>
        Task UpdateAsync(BackgroundJobInfo jobInfo);
    }
}