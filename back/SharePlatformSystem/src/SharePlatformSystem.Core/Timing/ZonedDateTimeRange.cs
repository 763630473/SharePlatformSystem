using SharePlatformSystem.Core.Timing.Timezone;
using System;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    ///���ڴ洢����ʱ�������ڷ�Χ��<see cref="IZonedDateTimeRange"/>����ʵ�֡�
    /// Ĭ��ʱ��ΪUTC
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
        /// ����ʱ�䷶Χ��ʱ��
        /// </summary>
        public string Timezone { get; set; } = "UTC";

        /// <summary>
        /// ��ƫ�ƵĿ�ʼʱ��
        /// </summary>
        public DateTimeOffset StartTimeOffset
        {
            get => TimezoneHelper.ConvertToDateTimeOffset(StartTime, Timezone);
            set => StartTimeUtc = value.UtcDateTime;
        }

        /// <summary>
        /// ��ƫ�ƵĽ���ʱ��
        /// </summary>
        public DateTimeOffset EndTimeOffset
        {
            get => TimezoneHelper.ConvertToDateTimeOffset(EndTime, Timezone);
            set => EndTimeUtc = value.UtcDateTime;
        }

        /// <summary>
        ///��UTC��ʾ�Ŀ�ʼʱ��
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
        ///��UTC��ʾ�Ľ���ʱ��
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
        /// ����ʱ���ĵ�ǰʱ��
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
        /// ��ȡ��ʾ����ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ����ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ����ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ�ϸ��µķ������ڷ�Χ��
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
        /// ��ȡ��ʾ���µķ������ڷ�Χ��
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
        ///��ȡ��ʾ�¸��µķ������ڷ�Χ��
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
        /// ��ȡ��ʾȥ��ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ����ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ��һ��ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ��ȥ30�죨30x24Сʱ����������ķ������ڷ�Χ��
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
        ///��ȡ��ʾ��ȥ30�죨���������죩�ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ��ȥ7�죨7x24Сʱ����������ķ������ڷ�Χ��
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
        /// ��ȡ��ʾ��ȥ7�죨���������죩�ķ������ڷ�Χ��
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
