using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.BackgroundJobs
{
    [Serializable]
    public class BackgroundJobException : SharePlatformException
    {
        [CanBeNull]
        public BackgroundJobInfo BackgroundJob { get; set; }

        [CanBeNull]
        public object JobObject { get; set; }

        /// <summary>
        /// 创建一个新的"BackgroundJobException"对象实例.
        /// </summary>
        public BackgroundJobException()
        {

        }

        /// <summary>
        /// 创建新的“BackgroundJobException”对象。
        /// </summary>
        public BackgroundJobException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 创建新的“BackgroundJobException”对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部例外</param>
        public BackgroundJobException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
