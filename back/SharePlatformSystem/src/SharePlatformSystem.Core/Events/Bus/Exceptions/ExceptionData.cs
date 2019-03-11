using System;

namespace SharePlatformSystem.Events.Bus.Exceptions
{
    /// <summary>
    /// 此类事件可用于通知异常。
    /// </summary>
    public class ExceptionData : EventData
    {
        /// <summary>
        /// 异常对象。
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        ///构造函数。
        /// </summary>
        /// <param name="exception">异常对象</param>
        public ExceptionData(Exception exception)
        {
            Exception = exception;
        }
    }
}
