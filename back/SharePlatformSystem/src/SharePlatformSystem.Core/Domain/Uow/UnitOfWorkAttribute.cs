using System;
using System.Transactions;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///����������ָʾ����������ԭ�ӷ�����Ӧ������Ϊ������Ԫ��
    ///�ػ���д����Եķ����������ݿ����ӣ����ڵ��ø÷���֮ǰ��������
    ///�������ý���ʱ���ύ�������û���쳣�������и���Ӧ�õ����ݿ⣬
    ///�����ع���
    /// </summary>
    /// <remarks>
    ///����ڵ��ô˷���֮ǰ����һ��������Ԫ�����������Ч��������ڣ�����ʹ����ͬ������
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// ��Χѡ�
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        ///����UOW�����Ե���
        ///���δ�ṩ����ʹ��Ĭ��ֵ��
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        ///UOW��ʱΪ���롣
        ///���δ�ṩ����ʹ��Ĭ��ֵ��
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        ///�����UOW�������Եģ����ѡ��ָʾ����ĸ��뼶��
        ///���δ�ṩ����ʹ��Ĭ��ֵ��
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        ///���ڷ�ֹ���������Ĺ�����Ԫ��
        ///����Ѿ��������Ĺ�����Ԫ������Դ����ԡ�
        ///Ĭ��ֵ��false��
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        ///�����µ�UnitOfWorkAttribute����
        /// </summary>
        public UnitOfWorkAttribute()
        {

        }

        /// <summary>
        /// �����µġ�UnitOfWorkAttribute������
        /// </summary>
        /// <param name="isTransactional">
        /// �˹�����Ԫ�Ƿ��������Եģ�
        /// </param>
        public UnitOfWorkAttribute(bool isTransactional)
        {
            IsTransactional = isTransactional;
        }

        /// <summary>
        ///�����µġ�UnitOfWorkAttribute������
        /// </summary>
        /// <param name="timeout">����</param>
        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        ///�����µġ�UnitOfWorkAttribute������
        /// </summary>
        /// <param name="isTransactional">�˹�����Ԫ�Ƿ��������Եģ�</param>
        /// <param name="timeout">����</param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// �����µġ�UnitOfWorkAttribute������
        /// <see cref="IsTransactional"/> �Զ�����Ϊ�档
        /// </summary>
        /// <param name="isolationLevel">������뼶��</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// �����µġ�UnitOfWorkAttribute������
        /// <see cref="IsTransactional"/> �Զ�����Ϊ�档
        /// </summary>
        /// <param name="isolationLevel">������뼶��</param>
        /// <param name="timeout">����ʱ�����룩</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        ///�����µġ�UnitOfWorkAttribute������
        /// <see cref="IsTransactional"/> �Զ�����Ϊ�档
        /// </summary>
        /// <param name="scope">���׷�Χ</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = true;
            Scope = scope;
        }

        /// <summary>
        /// �����µġ�UnitOfWorkAttribute������
        /// </summary>
        /// <param name="scope">����Χ</param>
        /// <param name="isTransactional">
        /// �˹�����Ԫ�Ƿ��������Եģ�
        /// </param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, bool isTransactional)
        {
            Scope = scope;
            IsTransactional = isTransactional;
        }

        /// <summary>
        ///�����µġ�UnitOfWorkAttribute������
        /// <see cref="IsTransactional"/>�Զ�����Ϊ�档
        /// </summary>
        /// <param name="scope">���׷�Χ</param>
        /// <param name="timeout">����ʱ�����룩</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}