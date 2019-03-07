using System;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///���幤����λ��
    ///�˽ӿ���SharePlatform�ڲ�ʹ�á�
    ///ʹ�á�iunitofWorkManager.begin�����������µĹ�����Ԫ��
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// ��UOW��ΨһID��
        /// </summary>
        string Id { get; }

        /// <summary>
        /// �����ⲿUOW��������ڣ���
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// ʹ�ø�����ѡ�ʼ������Ԫ��
        /// </summary>
        /// <param name="options">������λѡ��</param>
        void Begin(UnitOfWorkOptions options);
    }
}