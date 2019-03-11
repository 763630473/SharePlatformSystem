using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    /// 为具有时区的日期时间范围定义接口。
    /// </summary>
    public interface IZonedDateTimeRange : IDateTimeRange
    {
        /// <summary>
        /// 日期时间范围的时区
        /// </summary>
        string Timezone { get; set; }

        /// <summary>
        /// 带偏移的开始时间
        /// </summary>
        DateTimeOffset StartTimeOffset { get; set; }

        /// <summary>
        /// 带偏移的结束时间
        /// </summary>
        DateTimeOffset EndTimeOffset { get; set; }

        /// <summary>
        /// 以UTC表示的开始时间
        /// </summary>
        DateTime StartTimeUtc { get; set; }

        /// <summary>
        ///以UTC表示的结束时间
        /// </summary>
        DateTime EndTimeUtc { get; set; }
    }
}
