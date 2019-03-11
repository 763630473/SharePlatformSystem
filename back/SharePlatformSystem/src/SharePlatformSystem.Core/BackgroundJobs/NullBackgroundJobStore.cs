using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// ��ibackgroundjobstore���Ŀ�ģʽʵ�֡�
    ///�����ibackgroundjobstore��������ʵ�ʵĳ־ô洢ʵ�֣���ʹ����
    ///����û��ΪӦ�ó���������ҵִ�У���ibackgroundJobConfiguration.IsJobExecutionEnabled������
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