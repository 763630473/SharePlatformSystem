using System.Transactions;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///工作单元管理器。
    ///用于开始和控制一个工作单元。
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        ///获取当前活动的工作单元（如果不存在，则为空）。
        /// </summary>
        IActiveUnitOfWork Current { get; }

        /// <summary>
        ///开始新的工作单元。
        /// </summary>
        /// <returns>能够完成工作单元的手柄</returns>
        IUnitOfWorkCompleteHandle Begin();

        /// <summary>
        ///开始新的工作单元。
        /// </summary>
        /// <returns>能够完成工作单元的手柄</returns>
        IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope);

        /// <summary>
        ///开始新的工作单元。
        /// </summary>
        /// <returns>能够完成工作单元的手柄</returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options);
    }
}
