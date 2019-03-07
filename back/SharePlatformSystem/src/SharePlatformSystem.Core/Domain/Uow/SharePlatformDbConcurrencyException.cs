using SharePlatformSystem.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SharePlatformSystem.Domain.Uow
{
    [Serializable]
    public class SharePlatformDbConcurrencyException : SharePlatformException
    {
        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        public SharePlatformDbConcurrencyException()
        {

        }

        /// <summary>
        //创建一个新的对象。
        /// </summary>
        public SharePlatformDbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        public SharePlatformDbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public SharePlatformDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}