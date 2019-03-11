using System;

namespace SharePlatformSystem.Events.Bus.Exceptions
{
    /// <summary>
    /// 此类事件用于通知由SharePlatform基础结构处理的异常。
    /// </summary>
    public class SharePlatformHandledExceptionData : ExceptionData
    {
        /// <summary>
        ///构造函数。
        /// </summary>
        /// <param name="exception">异常对象</param>
        public SharePlatformHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}