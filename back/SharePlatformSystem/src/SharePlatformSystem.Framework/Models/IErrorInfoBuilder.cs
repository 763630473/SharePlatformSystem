using System;

namespace SharePlatformSystem.Framework.Models
{
    /// <summary>
    /// 此接口用于生成对象。
    /// </summary>
    public interface IErrorInfoBuilder
    {
        /// <summary>
        /// 使用给定的<paramref name=“exception”/>对象创建<see cref=“errorinfo”/>的新实例。
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <returns>已创建<see cref=“errorinfo”/>对象</returns>
        ErrorInfo BuildForException(Exception exception);

        /// <summary>
        /// 添加一个对象。
        /// </summary>
        /// <param name="converter">转换器</param>
        void AddExceptionConverter(IExceptionToErrorInfoConverter converter);
    }
}