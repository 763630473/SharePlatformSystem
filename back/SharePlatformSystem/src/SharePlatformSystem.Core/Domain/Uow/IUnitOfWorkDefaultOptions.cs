using System;
using System.Collections.Generic;
using System.Transactions;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// 用于获取/设置工作单元的默认选项。
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// 范围选项。
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// 如果工作单元是事务性的。
        ///默认值：真。
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        ///布尔值表示System.Transactions.TransactionScope可用于当前应用程序。
        ///默认值：真。
        /// </summary>
        bool IsTransactionScopeAvailable { get; set; }

        /// <summary>
        /// 获取/设置工作单位的超时值。
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        ///获取/设置事务的隔离级别。
        ///如果“IsTransactional”为真，则使用此选项。
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets list of all data filter configurations.
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// 用于确定常规工作单元类的选择器列表。
        /// </summary>
        List<Func<Type, bool>> ConventionalUowSelectors { get; }

        /// <summary>
        /// 向工作单元系统注册数据过滤器。
        /// </summary>
        /// <param name="filterName">筛选器的名称。</param>
        /// <param name="isEnabledByDefault">默认情况下是否启用筛选器。</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// 重写数据筛选器定义。
        /// </summary>
        /// <param name="filterName">筛选器的名称。</param>
        /// <param name="isEnabledByDefault">默认情况下是否启用筛选器。</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}