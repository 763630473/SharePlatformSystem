using System;
using System.Threading.Tasks;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///�������һ��������Ԫ��
    ///�޷�ע���ֱ��ʹ�ô˽ӿڡ�
    ///��Ϊʹ�á�iunitofworkmanager����
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        ///��ɴ˹�����Ԫ��
        ///�������и��Ĳ��ύ����������ڣ���
        /// </summary>
        void Complete();

        /// <summary>
        ///��ɴ˹�����Ԫ��
        ///�������и��Ĳ��ύ����������ڣ���
        /// </summary>
        Task CompleteAsync();
    }
}