using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    /// <see cref=“datetime”/>的扩展方法。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 将日期时间转换为UNIX时间戳
        /// </summary>
        /// <param name="target">此日期时间</param>
        /// <returns></returns>
        public static double ToUnixTimestamp(this DateTime target)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = target - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// 将中的Unix时间戳转换为日期时间
        /// </summary>
        /// <param name="unixTime">这个Unix时间戳</param>
        /// <returns></returns>
        public static DateTime FromUnixTimestamp(this double unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return epoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// 获取当天结束时的值（23:59）
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToDayEnd(this DateTime target)
        {
            return target.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// 获取指定日期的一周的第一个日期
        /// </summary>
        /// <param name="dt">此日期时间</param>
        /// <param name="startOfWeek">一周的开始日（即星期日/星期一）</param>
        /// <returns>一周的第一天</returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;

            if (diff < 0)
                diff += 7;

            return dt.AddDays(-1 * diff).Date;
        }

        /// <summary>
        ///返回一个月中的所有日期。
        /// </summary>
        /// <param name="year">这一年。</param>
        /// <param name="month">这个月。</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysOfMonth(int year, int month)
        {
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day + 1));
        }

        /// <summary>
        /// 确定一个月内日期的DayOfWeek的第n个实例
        /// </summary>
        /// <returns></returns>
        /// <example>11/29/2011  会返回5，因为现在是每个月的第5个星期二</example>
        public static int WeekDayInstanceOfMonth(this DateTime dateTime)
        {
            var y = 0;
            return DaysOfMonth(dateTime.Year, dateTime.Month)
                .Where(date => dateTime.DayOfWeek.Equals(date.DayOfWeek))
                .Select(x => new { n = ++y, date = x })
                .Where(x => x.date.Equals(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day)))
                .Select(x => x.n).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个月内的总天数
        /// </summary>
        /// <param name="dateTime">日期时间。</param>
        /// <returns></returns>
        public static int TotalDaysInMonth(this DateTime dateTime)
        {
            return DaysOfMonth(dateTime.Year, dateTime.Month).Count();
        }

        /// <summary>
        /// 接受任何日期并将其值作为未指定的日期时间返回
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeUnspecified(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                return date;
            }

            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// 缩短日期时间的毫秒数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime TrimMilliseconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        }
    }
}