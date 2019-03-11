using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    /// SharePlatform系统针对特定于SharePlatform的异常引发这些异常的基本异常类型。
    /// </summary>
    [Serializable]
    public class SharePlatformException : Exception
    {
        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        public SharePlatformException()
        {

        }

        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        public SharePlatformException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        public SharePlatformException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public SharePlatformException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
