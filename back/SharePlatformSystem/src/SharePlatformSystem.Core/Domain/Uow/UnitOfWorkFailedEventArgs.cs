using System;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// 用作“iaActiveUnitOfWork.Failed”事件的事件参数。
    /// </summary>
    public class UnitOfWorkFailedEventArgs : EventArgs
    {
        /// <summary>
        ///导致失败的异常。
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 创建新的“UnitOfWorkfailedEventargs”对象。
        /// </summary>
        /// <param name="exception">导致失败的异常</param>
        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
