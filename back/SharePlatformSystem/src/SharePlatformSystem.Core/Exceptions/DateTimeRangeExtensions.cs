using System;
using System.Collections.Generic;
using System.Linq;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    /// <see cref=“idateTimeRange”/>的扩展方法。
    /// </summary>
    public static class DateTimeRangeExtensions
    {
        /// <summary>
        ///将日期范围设置为给定目标。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void SetTo(this IDateTimeRange source, IDateTimeRange target)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }

        /// <summary>
        /// 从给定源设置日期范围。
        /// </summary>
        public static void SetFrom(this IDateTimeRange target, IDateTimeRange source)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }

        /// <summary>
        /// 返回日期时间范围的所有日期。
        /// </summary>
        /// <param name="dateRange">日期范围。</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysInRange(this IDateTimeRange dateRange)
        {
            return Enumerable.Range(0, (dateRange.TimeSpan).Days)
                .Select(offset => new DateTime(
                    dateRange.StartTime.AddDays(offset).Year,
                    dateRange.StartTime.AddDays(offset).Month,
                    dateRange.StartTime.AddDays(offset).Day));
        }

        /// <summary>
        /// 返回范围内的所有日期。
        /// </summary>
        /// <param name="start">开始。</param>
        /// <param name="end">结束。</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysInRange(DateTime start, DateTime end)
        {
            return new DateTimeRange(start, end).DaysInRange();
        }
    }
}