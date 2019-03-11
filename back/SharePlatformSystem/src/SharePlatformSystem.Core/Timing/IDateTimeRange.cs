using System;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    /// 定义日期时间范围的接口。
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        ///日期时间范围的开始时间。
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// 日期时间范围的结束时间。
        /// </summary>
        DateTime EndTime { get; set; }

        /// <summary>
        /// 开始时间和结束时间之间的时差。
        /// </summary>
        TimeSpan TimeSpan { get; set; }
    }
}
