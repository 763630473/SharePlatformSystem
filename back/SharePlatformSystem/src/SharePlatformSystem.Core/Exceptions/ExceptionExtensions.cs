using System;
using System.Runtime.ExceptionServices;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    ///类的扩展方法。
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///使用<see cref=“exceptionDispatchInfo.capture”/>方法重新引发异常
        ///同时保留堆栈跟踪。
        /// </summary>
        /// <param name="exception">要重新引发的异常</param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}