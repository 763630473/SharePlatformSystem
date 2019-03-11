using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.BackgroundJobs
{
    /// <summary>
    /// �������ڴ洢/��ȡ��̨��ҵ�Ľӿڡ�
    /// </summary>
    public interface IBackgroundJobStore
    {
        /// <summary>
        /// ���ڸ�����JobID��ȡBackgroundJobInfo��
        /// </summary>
        /// <param name="jobId">��ҵΨһ��ʶ����</param>
        /// <returns>BackgroundJobInfo����</returns>
        Task<BackgroundJobInfo> GetAsync(string jobId);

        /// <summary>
        ///�����̨��ҵ��
        /// </summary>
        /// <param name="jobInfo">������Ϣ��</param>
        Task InsertAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        ///��ȡ�ȴ�����ҵ����Ӧ�ø�������������ù�����
        ///��������isabandoned��nexttrytime&lt��=clock.now��
        ///�������ݣ�priority desc��trycount asc��nexttrytime asc��
        ///���������maxResultCount����
        /// </summary>
        /// <param name="maxResultCount">�����������</param>
        Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount);

        /// <summary>
        /// ɾ����ҵ��
        /// </summary>
        /// <param name="jobInfo">Job information.</param>
        Task DeleteAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// ������ҵ��
        /// </summary>
        /// <param name="jobInfo">������Ϣ��</param>
        Task UpdateAsync(BackgroundJobInfo jobInfo);
    }
}