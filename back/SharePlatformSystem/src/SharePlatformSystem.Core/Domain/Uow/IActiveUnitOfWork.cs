using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///此接口用于使用活动工作单元。
    ///无法注入此接口。
    ///改为使用“iunitofworkmanager”。
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        ///此UOW成功完成时引发此事件。
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        ///此UOW失败时引发此事件。
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 释放此UOW时引发此事件。
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// 获取此工作单元是否是事务性的。
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        ///获取此工作单元的数据筛选器配置。
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        ///用于在UnitOfWork上进行自定义操作的字典
        /// </summary>
        Dictionary<string, object> Items { get; set; }

        /// <summary>
        /// 此UOW是否已处理？
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        ///在此工作单元中保存到现在为止的所有更改。
        ///可以调用此方法以在需要时应用更改。
        ///请注意，如果此工作单元是事务性的，则如果事务回滚，则保存的更改也将回滚。
        ///一般不需要显式调用来保存更改，
        ///因为所有更改都是在工作单元结束时自动保存的。
        /// </summary>
        void SaveChanges();

        /// <summary>
        ///在此工作单元中保存到现在为止的所有更改。
        ///可以调用此方法以在需要时应用更改。
        ///请注意，如果此工作单元是事务性的，则如果事务回滚，则保存的更改也将回滚。
        ///一般不需要显式调用来保存更改，
        ///因为所有更改都是在工作单元结束时自动保存的。
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        ///禁用一个或多个数据筛选器。
        ///如果过滤器已被禁用，则不执行任何操作。
        ///在using语句中使用此方法在需要时重新启用筛选器。
        /// </summary>
        /// <param name="filterNames">一个或多个筛选器名称。“用于标准筛选器的SharePlatformDataFilters。</param>
        /// <returns>A <see cref="IDisposable"/>手柄恢复禁用效果。</returns>
        IDisposable DisableFilter(params string[] filterNames);

        /// <summary>
        ///启用一个或多个数据筛选器。
        ///对已启用的筛选器不执行任何操作。
        ///如果需要，可以在using语句中使用此方法重新禁用筛选器。
        /// </summary>
        /// <param name="filterNames">一个或多个筛选器名称。“用于标准筛选器的SharePlatformDataFilters。</param>
        /// <returns>一个“idisposable”句柄来恢复启用效果。</returns>
        IDisposable EnableFilter(params string[] filterNames);

        /// <summary>
        ///检查是否启用了筛选器。
        /// </summary>
        /// <param name="filterName">筛选器的名称。“用于标准筛选器的SharePlatformDataFilters。</param>
        bool IsFilterEnabled(string filterName);

        /// <summary>
        ///设置（覆盖）过滤器参数的值。
        /// </summary>
        /// <param name="filterName">筛选器的名称</param>
        /// <param name="parameterName">参数的名称</param>
        /// <param name="value">要设置的参数的值</param>
        IDisposable SetFilterParameter(string filterName, string parameterName, object value);
    }
}