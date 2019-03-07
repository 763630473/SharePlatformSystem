using System;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///定义工作单位。
    ///此接口由SharePlatform内部使用。
    ///使用“iunitofWorkManager.begin（）”启动新的工作单元。
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// 此UOW的唯一ID。
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 引用外部UOW（如果存在）。
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 使用给定的选项开始工作单元。
        /// </summary>
        /// <param name="options">工作单位选项</param>
        void Begin(UnitOfWorkOptions options);
    }
}