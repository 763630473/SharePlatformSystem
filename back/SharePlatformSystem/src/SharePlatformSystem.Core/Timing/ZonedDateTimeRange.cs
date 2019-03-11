using SharePlatformSystem.Core.Timing.Timezone;
using System;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    ///用于存储带有时区的日期范围的<see cref="IZonedDateTimeRange"/>基本实现。
    /// 默认时区为UTC
    /// </summary>
    public class ZonedDateTimeRange : DateTimeRange, IZonedDateTimeRange
    {
        public ZonedDateTimeRange()
        {
            
        }

        public ZonedDateTimeRange(string timezone)
        {
            Timezone = timezone;
        }

        public ZonedDateTimeRange(IDateTimeRange dateTimeRange, string timeZoneId) : base(dateTimeRange)
        {
            Timezone = timeZoneId;
        }

        public ZonedDateTimeRange(IZonedDateTimeRange zonedDateTimeRange) : base(zonedDateTimeRange)
        {
            Timezone = zonedDateTimeRange.Timezone;
        }

        public ZonedDateTimeRange(DateTime startTime, DateTime endTime, string timeZoneId) : base(startTime, endTime)
        {
            Timezone = timeZoneId;
        }

        public ZonedDateTimeRange(DateTime startTime, TimeSpan timeSpan, string timeZoneId) : base(startTime, timeSpan)
        {
            Timezone = timeZoneId;
        }

        /// <summary>
        /// 日期时间范围的时区
        /// </summary>
        public string Timezone { get; set; } = "UTC";

        /// <summary>
        /// 带偏移的开始时间
        /// </summary>
        public DateTimeOffset StartTimeOffset
        {
            get => TimezoneHelper.ConvertToDateTimeOffset(StartTime, Timezone);
            set => StartTimeUtc = value.UtcDateTime;
        }

        /// <summary>
        /// 带偏移的结束时间
        /// </summary>
        public DateTimeOffset EndTimeOffset
        {
            get => TimezoneHelper.ConvertToDateTimeOffset(EndTime, Timezone);
            set => EndTimeUtc = value.UtcDateTime;
        }

        /// <summary>
        ///以UTC表示的开始时间
        /// </summary>
        public DateTime StartTimeUtc
        {
            get => StartTimeOffset.UtcDateTime;
            set
            {
                var localized = TimezoneHelper.ConvertFromUtc(value, Timezone);
                if (localized.HasValue)
                {
                    StartTime = localized.Value;
                }
            }
        }

        /// <summary>
        ///以UTC表示的结束时间
        /// </summary>
        public DateTime EndTimeUtc
        {
            get => EndTimeOffset.UtcDateTime;
            set
            {
                var localized = TimezoneHelper.ConvertFromUtc(value, Timezone);
                if (localized.HasValue)
                {
                    EndTime = localized.Value;
                }
            }
        }

        /// <summary>
        /// 基于时区的当前时间
        /// </summary>
        public DateTime Now
        {
            get
            {
                DateTime? localTime;
                switch (Clock.Kind)
                {
                    case DateTimeKind.Local:
                        localTime = TimezoneHelper.ConvertFromUtc(Clock.Now.ToUniversalTime(), Timezone);
                        break;
                    case DateTimeKind.Unspecified:
                        localTime = Clock.Now;
                        break;
                    case DateTimeKind.Utc:
                        localTime = TimezoneHelper.ConvertFromUtc(Clock.Now, Timezone);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return localTime ?? Clock.Now;
            }
        }

        /// <summary>
        /// 获取表示昨天的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange Yesterday
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(now.Date.AddDays(-1), now.Date.AddMilliseconds(-1), Timezone);
            }
        }

        /// <summary>
        /// 获取表示今天的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange Today
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(now.Date, now.Date.AddDays(1).AddMilliseconds(-1), Timezone);
            }
        }

        /// <summary>
        /// 获取表示明天的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange Tomorrow
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(now.Date.AddDays(1), now.Date.AddDays(2).AddMilliseconds(-1), Timezone);
            }
        }

        /// <summary>
        /// 获取表示上个月的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange LastMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(-1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new ZonedDateTimeRange(startTime, endTime, Timezone);
            }
        }

        /// <summary>
        /// 获取表示此月的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange ThisMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new ZonedDateTimeRange(startTime, endTime, Timezone);
            }
        }

        /// <summary>
        ///获取表示下个月的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange NextMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new ZonedDateTimeRange(startTime, endTime, Timezone);
            }
        }


        /// <summary>
        /// 获取表示去年的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange LastYear
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(new DateTime(now.Year - 1, 1, 1), new DateTime(now.Year, 1, 1).AddMilliseconds(-1), Timezone);
            }
        }

        /// <summary>
        /// 获取表示今年的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange ThisYear
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(new DateTime(now.Year, 1, 1), new DateTime(now.Year + 1, 1, 1).AddMilliseconds(-1), Timezone);
            }
        }

        /// <summary>
        /// 获取表示下一年的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange NextYear
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(new DateTime(now.Year + 1, 1, 1), new DateTime(now.Year + 2, 1, 1).AddMilliseconds(-1), Timezone);
            }
        }


        /// <summary>
        /// 获取表示过去30天（30x24小时）包括今天的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange Last30Days
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(now.AddDays(-30), now, Timezone);
            }
        }

        /// <summary>
        ///获取表示过去30天（不包括今天）的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange Last30DaysExceptToday
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(now.Date.AddDays(-30), now.Date.AddMilliseconds(-1), Timezone);
            }
        }

        /// <summary>
        /// 获取表示过去7天（7x24小时）包括今天的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange Last7Days
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(now.AddDays(-7), now, Timezone);
            }
        }

        /// <summary>
        /// 获取表示过去7天（不包括今天）的分区日期范围。
        /// </summary>
        public new ZonedDateTimeRange Last7DaysExceptToday
        {
            get
            {
                var now = Now;
                return new ZonedDateTimeRange(now.Date.AddDays(-7), now.Date.AddMilliseconds(-1), Timezone);
            }
        }
    }
}
