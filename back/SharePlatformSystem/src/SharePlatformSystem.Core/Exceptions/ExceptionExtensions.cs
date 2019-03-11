using System;
using System.Runtime.ExceptionServices;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    ///�����չ������
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///ʹ��<see cref=��exceptionDispatchInfo.capture��/>�������������쳣
        ///ͬʱ������ջ���١�
        /// </summary>
        /// <param name="exception">Ҫ�����������쳣</param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}