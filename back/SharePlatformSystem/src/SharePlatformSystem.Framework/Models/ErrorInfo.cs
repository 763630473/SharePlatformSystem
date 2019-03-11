using System;

namespace SharePlatformSystem.Framework.Models
{
    /// <summary>
    ///用于存储有关错误的信息。
    /// </summary>
    [Serializable]
    public class ErrorInfo
    {
        /// <summary>
        /// 错误代码。
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误详细信息。
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 创建<see cref=“errorinfo”/>的新实例。
        /// </summary>
        public ErrorInfo()
        {

        }

        /// <summary>
        /// 创建<see cref=“errorinfo”/>的新实例。
        /// </summary>
        /// <param name="message">错误消息</param>
        public ErrorInfo(string message)
        {
            Message = message;
        }

        /// <summary>
        ///创建<see cref=“errorinfo”/>的新实例。
        /// </summary>
        /// <param name="code">错误代码</param>
        public ErrorInfo(int code)
        {
            Code = code;
        }

        /// <summary>
        /// 创建<see cref=“errorinfo”/>的新实例。
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误消息</param>
        public ErrorInfo(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// 创建<see cref=“errorinfo”/>的新实例。
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="details">错误详情</param>
        public ErrorInfo(string message, string details)
            : this(message)
        {
            Details = details;
        }

        /// <summary>
        /// 创建<see cref=“errorinfo”/>的新实例。
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误消息</param>
        /// <param name="details">错误详情</param>
        public ErrorInfo(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }
    }
}