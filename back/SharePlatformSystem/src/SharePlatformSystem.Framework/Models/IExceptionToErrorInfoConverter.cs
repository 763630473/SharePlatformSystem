using System;

namespace SharePlatformSystem.Framework.Models
{
    /// <summary>
    ///此接口可以实现将<see cref=“exception”/>对象转换为<see cref=“errorinfo”/>对象。
    ///实现责任链模式。
    /// </summary>
    public interface IExceptionToErrorInfoConverter
    {
        /// <summary>
        ///下一个转换器。如果此转换器确定此异常未知，则可以调用Next.Convert（…）。
        /// </summary>
        IExceptionToErrorInfoConverter Next { set; }

        /// <summary>
        /// 转换器方法。
        /// </summary>
        /// <param name="exception">例外</param>
        /// <returns>错误信息或空</returns>
        ErrorInfo Convert(Exception exception);
    }
}