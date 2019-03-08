using System;
using System.Collections.Generic;
using System.Transactions;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// ���ڻ�ȡ/���ù�����Ԫ��Ĭ��ѡ�
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// ��Χѡ�
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// ���������Ԫ�������Եġ�
        ///Ĭ��ֵ���档
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        ///����ֵ��ʾSystem.Transactions.TransactionScope�����ڵ�ǰӦ�ó���
        ///Ĭ��ֵ���档
        /// </summary>
        bool IsTransactionScopeAvailable { get; set; }

        /// <summary>
        /// ��ȡ/���ù�����λ�ĳ�ʱֵ��
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        ///��ȡ/��������ĸ��뼶��
        ///�����IsTransactional��Ϊ�棬��ʹ�ô�ѡ�
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets list of all data filter configurations.
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// ����ȷ�����湤����Ԫ���ѡ�����б�
        /// </summary>
        List<Func<Type, bool>> ConventionalUowSelectors { get; }

        /// <summary>
        /// ������Ԫϵͳע�����ݹ�������
        /// </summary>
        /// <param name="filterName">ɸѡ�������ơ�</param>
        /// <param name="isEnabledByDefault">Ĭ��������Ƿ�����ɸѡ����</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// ��д����ɸѡ�����塣
        /// </summary>
        /// <param name="filterName">ɸѡ�������ơ�</param>
        /// <param name="isEnabledByDefault">Ĭ��������Ƿ�����ɸѡ����</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}