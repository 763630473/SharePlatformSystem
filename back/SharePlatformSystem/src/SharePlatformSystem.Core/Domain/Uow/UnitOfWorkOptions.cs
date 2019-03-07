using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// ������λѡ�
    /// </summary>
    public class UnitOfWorkOptions
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
        ///��ѡ��Ӧ����Ϊ��transactionscopeasyncflowOption.enabled��
        ///������첽��������ʹ�ù�����Ԫ��
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }

        /// <summary>
        /// ����������/����ĳЩɸѡ����
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; }

        /// <summary>
        ///�����µġ�UnitOfWorkOptions������
        /// </summary>
        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }

        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            //TODO:������ѡ�����..��
            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }

            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }

            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }
        }

        internal void FillOuterUowFiltersForNonProvidedOptions(List<DataFilterConfiguration> filterOverrides)
        {
            foreach (var filterOverride in filterOverrides)
            {
                if (FilterOverrides.Any(fo => fo.FilterName == filterOverride.FilterName))
                {
                    continue;
                }

                FilterOverrides.Add(filterOverride);
            }
        }
    }
}