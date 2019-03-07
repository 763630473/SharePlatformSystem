using System;
using System.Threading.Tasks;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///用于完成一个工作单元。
    ///无法注入或直接使用此接口。
    ///改为使用“iunitofworkmanager”。
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        ///完成此工作单元。
        ///保存所有更改并提交事务（如果存在）。
        /// </summary>
        void Complete();

        /// <summary>
        ///完成此工作单元。
        ///保存所有更改并提交事务（如果存在）。
        /// </summary>
        Task CompleteAsync();
    }
}