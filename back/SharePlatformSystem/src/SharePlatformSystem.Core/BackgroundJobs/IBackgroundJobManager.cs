using SharePlatformSystem.Threading.BackgroundWorkers;
using System;
using System.Threading.Tasks;
namespace SharePlatformSystem.BackgroundJobs
{
    //TODO: ΪIBackgroundJobManager�����Ƿ���EnqueueAsync��չ�������÷�����������Ϊ��������������Ƿ��Ͳ�����
    /// <summary>
    /// ������ҵ�������Ľӿڡ�
    /// </summary>
    public interface IBackgroundJobManager : IBackgroundWorker
    {
        /// <summary>
        /// ��Ҫִ�е���ҵ�Ŷӡ�
        /// </summary>
        /// <typeparam name="TJob">��ҵ�����͡�</typeparam>
        /// <typeparam name="TArgs">��ҵ���������͡�</typeparam>
        /// <param name="args">����������</param>
        /// <param name="priority">�������ȼ���</param>
        /// <param name="delay">��ҵ�ӳ٣���һ�γ���ǰ�ĵȴ�ʱ�䣩��</param>
        /// <returns>��̨��ҵ��Ψһ��ʶ����</returns>
        Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>;

        /// <summary>
        /// ɾ������ָ��jobid����ҵ��
        /// </summary>
        /// <param name="jobId">��ҵΨһ��ʶ����</param>
        /// <returns><c>True</c> �ڳɹ�����ȫ״̬ת��ʱ��<c>false<c>����</returns>
        Task<bool> DeleteAsync(string jobId);
    }
}