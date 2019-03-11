using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// “ibackgroundjobstore”的空模式实现。
    ///如果“ibackgroundjobstore”不是由实际的持久存储实现，则使用它
    ///并且没有为应用程序启用作业执行（“ibackgroundJobConfiguration.IsJobExecutionEnabled”）。
    /// </summary>
    public class NullBackgroundJobStore : IBackgroundJobStore
    {
        public Task<BackgroundJobInfo> GetAsync(string jobId)
        {
            return Task.FromResult(new BackgroundJobInfo());
        }

        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        public Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            return Task.FromResult(new List<BackgroundJobInfo>());
        }

        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }
    }
}