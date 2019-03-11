using System;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    /// 存储日期范围的基本实现。
    /// </summary>
    [Serializable]
    public class DateTimeRange : IDateTimeRange
    {
        /// <summary>
        /// 日期时间范围的开始时间。
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 日期时间范围的结束时间。
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///获取日期时间范围的时间跨度。
        ///设置时，重新计算结束时间
        /// </summary>
        public TimeSpan TimeSpan
        {
            get => EndTime - StartTime;
            set => EndTime = StartTime.Add(value);
        }

        private static DateTime Now => Clock.Now;

        /// <summary>
        ///创建一个新的对象。
        /// </summary>
        public DateTimeRange()
        {

        }

        /// <summary>
        ///从给定的<paramref name=“starttime”/>和<paramref name=“endtime”/>创建新的<see cref=“datetimerange”/>对象。
        /// </summary>
        /// <param name="startTime">日期时间范围的开始时间</param>
        /// <param name="endTime">日期时间范围的结束时间</param>
        public DateTimeRange(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        /// <summary>
        /// 从给定的<paramref name=“starttime”/>和<paramref name=“TimeSpan”/>创建新的<see cref=“datetimerange”/>对象。
        /// </summary>
        /// <param name="startTime">日期时间范围的开始时间</param>
        /// <param name="timeSpan">计算结束时间的时间跨度</param>
        public DateTimeRange(DateTime startTime, TimeSpan timeSpan)
        {
            StartTime = startTime;
            TimeSpan = timeSpan;
        }

        /// <summary>
        ///从给定的对象创建一个新的对象。
        /// </summary>
        /// <param name="dateTimeRange">IDateTimeRange对象</param>
        public DateTimeRange(IDateTimeRange dateTimeRange)
        {
            StartTime = dateTimeRange.StartTime;
            EndTime = dateTimeRange.EndTime;
        }

        /// <summary>
        ///获取表示昨天的日期范围。
        /// </summary>
        public static DateTimeRange Yesterday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-1), now.Date.AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 获取表示今天的日期范围。
        /// </summary>
        public static DateTimeRange Today
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date, now.Date.AddDays(1).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 获取表示明天的日期范围。
        /// </summary>
        public static DateTimeRange Tomorrow
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(1), now.Date.AddDays(2).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 获取表示上个月的日期范围。
        /// </summary>
        public static DateTimeRange LastMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(-1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        /// <summary>
        /// 获取表示此月的日期范围。
        /// </summary>
        public static DateTimeRange ThisMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        /// <summary>
        ///获取表示下个月的日期范围。
        /// </summary>
        public static DateTimeRange NextMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }


        /// <summary>
        /// 获取表示去年的日期范围。
        /// </summary>
        public static DateTimeRange LastYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year - 1, 1, 1), new DateTime(now.Year, 1, 1).AddMilliseconds(-1));
            }
        }

        /// <summary>
        ///获取表示今年的日期范围。
        /// </summary>
        public static DateTimeRange ThisYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year, 1, 1), new DateTime(now.Year + 1, 1, 1).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 获取表示下一年的日期范围。
        /// </summary>
        public static DateTimeRange NextYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year + 1, 1, 1), new DateTime(now.Year + 2, 1, 1).AddMilliseconds(-1));
            }
        }


        /// <summary>
        /// 获取表示过去30天（30x24小时）包括今天的日期范围。
        /// </summary>
        public static DateTimeRange Last30Days
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.AddDays(-30), now);
            }
        }

        /// <summary>
        /// 获取表示过去30天（不包括今天）的日期范围。
        /// </summary>
        public static DateTimeRange Last30DaysExceptToday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-30), now.Date.AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 获取表示过去7天（7x24小时）包括今天的日期范围。
        /// </summary>
        public static DateTimeRange Last7Days
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.AddDays(-7), now);
            }
        }

        /// <summary>
        /// 获取表示过去7天（不包括今天）的日期范围。
        /// </summary>
        public static DateTimeRange Last7DaysExceptToday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-7), now.Date.AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 返回表示当前值的<see cref=“system.string”/>。
        /// </summary>
        /// <returns>a<see cref=“system.string”/>表示当前<see cref="DateTimeRange"/>.</returns>
        public override string ToString()
        {
            return string.Format("[{0} - {1}]", StartTime, EndTime);
        }
    }
}