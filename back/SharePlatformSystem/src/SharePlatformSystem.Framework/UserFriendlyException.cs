using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Framework.Models;
using SharePlatformSystem.Logging;
using System;
using System.Runtime.Serialization;

namespace SharePlatformSystem.Framework
{
    /// <summary>
    /// 此异常类型直接显示给用户。
    /// </summary>
    [Serializable]
    public class UserFriendlyException : SharePlatformException, IHasLogSeverity, IHasErrorCode
    {
        /// <summary>
        /// 有关异常的其他信息。
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// 任意错误代码。
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///异常的严重性。
        /// 默认值: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// 构造函数.
        /// </summary>
        public UserFriendlyException()
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// 用于序列化的构造函数。
        /// </summary>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常消息</param>
        public UserFriendlyException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="severity">异常严重性</param>
        public UserFriendlyException(string message, LogSeverity severity)
            : base(message)
        {
            Severity = severity;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">异常消息</param>
        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="details">有关异常的其他信息</param>
        public UserFriendlyException(string message, string details)
            : this(message)
        {
            Details = details;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">异常消息</param>
        /// <param name="details">有关异常的其他信息</param>
        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="details">有关异常的其他信息</param>
        /// <param name="innerException">内部异常</param>
        public UserFriendlyException(string message, string details, Exception innerException)
            : this(message, innerException)
        {
            Details = details;
        }
    }
}