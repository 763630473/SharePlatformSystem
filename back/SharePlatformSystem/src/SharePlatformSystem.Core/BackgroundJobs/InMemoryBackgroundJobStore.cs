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
    /// ��ibackgroundjobstore�����ڴ�ʵ�֡�
    ///�����ibackgroundjobstore��������ʵ�ʵĳ־ô洢ʵ�֣���ʹ����
    ///����ΪӦ�ó�����������ҵִ�У���ibackgroundJobConfiguration.IsJobExecutionEnabled������
    /// </summary>
    /// <summary>
    /// ��ibackgroundjobstore�����ڴ�ʵ�֡�
    /// �����ibackgroundjobstore��������ʵ�ʵĳ־ô洢ʵ�֣���ʹ����
    ///����ΪӦ�ó�����������ҵִ�У���ibackgroundJobConfiguration.IsJobExecutionEnabled������
    /// </summary>
    public class InMemoryBackgroundJobStore : IBackgroundJobStore
    {
        private readonly ConcurrentDictionary<string, BackgroundJobInfo> _jobs;

        /// <summary>
        /// ��ʼ����InMemoryBackgroundJobstore�������ʵ����
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
            jobInfo.Id = jobInfo.Id??Guid.NewGuid().ToString();//�����������ο�lastid����
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