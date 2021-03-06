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
    /// In memory implementation of <see cref="IBackgroundJobStore"/>.
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// and job execution is enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// </summary>
    /// <summary>
    /// In memory implementation of <see cref="IBackgroundJobStore"/>.
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// and job execution is enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// </summary>
    public class InMemoryBackgroundJobStore : IBackgroundJobStore
    {
        private readonly ConcurrentDictionary<string, BackgroundJobInfo> _jobs;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryBackgroundJobStore"/> class.
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
            jobInfo.Id = jobInfo.Id??Guid.NewGuid().ToString();//Interlocked.Increment(ref _lastId);
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