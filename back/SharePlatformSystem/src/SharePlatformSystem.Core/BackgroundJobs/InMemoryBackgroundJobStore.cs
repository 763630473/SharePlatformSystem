using SharePlatformSystem.Core.Timing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// “ibackgroundjobstore”的内存实现。
    ///如果“ibackgroundjobstore”不是由实际的持久存储实现，则使用它
    ///并且为应用程序启用了作业执行（“ibackgroundJobConfiguration.IsJobExecutionEnabled”）。
    /// </summary>
    /// <summary>
    /// “ibackgroundjobstore”的内存实现。
    /// 如果“ibackgroundjobstore”不是由实际的持久存储实现，则使用它
    ///并且为应用程序启用了作业执行（“ibackgroundJobConfiguration.IsJobExecutionEnabled”）。
    /// </summary>
    public class InMemoryBackgroundJobStore : IBackgroundJobStore
    {
        private readonly ConcurrentDictionary<string, BackgroundJobInfo> _jobs;

        /// <summary>
        /// 初始化“InMemoryBackgroundJobstore”类的新实例。
        /// </summary>
        public InMemoryBackgroundJobStore()
        {
            _jobs = new ConcurrentDictionary<string, BackgroundJobInfo>();
        }

        public Task<BackgroundJobInfo> GetAsync(string jobId)
        {
            return Task.FromResult(_jobs[jobId]);
        }

        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            jobInfo.Id = jobInfo.Id??Guid.NewGuid().ToString();//联锁增量（参考lastid）；
            _jobs[jobInfo.Id] = jobInfo;

            return Task.FromResult(0);
        }

        public Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            var waitingJobs = _jobs.Values
                .Where(t => !t.IsAbandoned && t.NextTryTime <= Clock.Now)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.TryCount)
                .ThenBy(t => t.NextTryTime)
                .Take(maxResultCount)
                .ToList();

            return Task.FromResult(waitingJobs);
        }

        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            _jobs.TryRemove(jobInfo.Id, out _);

            return Task.FromResult(0);
        }

        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            if (jobInfo.IsAbandoned)
            {
                return DeleteAsync(jobInfo);
            }

            return Task.FromResult(0);
        }
    }
}