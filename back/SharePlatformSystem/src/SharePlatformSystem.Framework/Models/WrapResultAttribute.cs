using System;

namespace SharePlatformSystem.Framework.Models
{
    /// <summary>
    /// 用于确定应如何在Web层上包装响应。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute : Attribute
    {
        /// <summary>
        /// 成功时总结结果。成功时总结结果。
        /// </summary>
        public bool WrapOnSuccess { get; set; }

        /// <summary>
        /// 错误时换行结果。
        /// </summary>
        public bool WrapOnError { get; set; }

        /// <summary>
        ///日志错误。
        /// Default: true.
        /// </summary>
        public bool LogError { get; set; }

        /// <summary>
        ///初始化类的新实例。
        /// </summary>
        /// <param name="wrapOnSuccess">成功时总结结果。</param>
        /// <param name="wrapOnError">错误时换行结果。</param>
        public WrapResultAttribute(bool wrapOnSuccess = true, bool wrapOnError = true)
        {
            WrapOnSuccess = wrapOnSuccess;
            WrapOnError = wrapOnError;

            LogError = true;
        }
    }
}
