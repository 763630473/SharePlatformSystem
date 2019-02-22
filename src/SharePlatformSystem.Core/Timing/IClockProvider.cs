using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    /// 定义接口以执行一些常见的日期时间操作。
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// 获取现在的时间
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// 获取时间的种类
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// 提供程序是否支持多个时区。
        /// </summary>
        bool SupportsMultipleTimezone { get; }

        /// <summary>
        /// 规范化给定的日期时间。
        /// </summary>
        /// <param name="dateTime">要规范化的日期时间。</param>
        /// <returns>规范时间</returns>
        DateTime Normalize(DateTime dateTime);
    }
}