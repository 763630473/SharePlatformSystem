using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    /// 如果SharePlatform初始化过程中出现问题，则会引发此异常。
    /// </summary>
    [Serializable]
    public class SharePlatformInitializationException : SharePlatformException
    {
        /// <summary>
        /// 构造器.
        /// </summary>
        public SharePlatformInitializationException()
        {

        }

        /// <summary>
        /// 用于序列化的构造函数。
        /// </summary>
        public SharePlatformInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 构造器.
        /// </summary>
        /// <param name="message">异常消息</param>
        public SharePlatformInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造器.
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public SharePlatformInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
