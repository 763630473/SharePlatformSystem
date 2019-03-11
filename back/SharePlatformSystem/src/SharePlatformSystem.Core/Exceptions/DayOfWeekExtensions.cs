using System;
using System.Linq;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    ///<see cref=“dayOfWeekExtensions”/>的扩展方法。
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        ///检查给定值是否为周末。
        /// </summary>
        public static bool IsWeekend(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        ///检查给定值是否为weekday。
        /// </summary>
        public static bool IsWeekday(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }

        /// <summary>
        /// 查找一个月的第n个星期日。
        /// </summary>
        /// <param name="dayOfWeek">一周中的某一天。</param>
        /// <param name="year">这一年。</param>
        /// <param name="month">这个月。</param>
        /// <param name="n">第n个实例。</param>
        /// <remarks>补偿每月第4天和第5天</remarks>
        public static DateTime FindNthWeekDayOfMonth(this DayOfWeek dayOfWeek, int year, int month, int n)
        {
            if (n < 1 || n > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            var y = 0;

            var daysOfMonth = DateTimeExtensions.DaysOfMonth(year, month);

            // 补偿“每月最后一天”
            var totalInstances = dayOfWeek.TotalInstancesInMonth(year, month);
            if (n == 5 && n > totalInstances)
                n = 4;

            var foundDate = daysOfMonth
                .Where(date => dayOfWeek.Equals(date.DayOfWeek))
                .OrderBy(date => date)
                .Select(x => new { n = ++y, date = x })
                .Where(x => x.n.Equals(n)).Select(x => x.date).First(); //black magic wizardry

            return foundDate;
        }

        /// <summary>
        /// 查找一个月内特定DayOfWeek的实例总数。
        /// </summary>
        /// <param name="dayOfWeek">一周中的某一天。</param>
        /// <param name="year">今年。</param>
        /// <param name="month">这个月。</param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, int year, int month)
        {
            return DateTimeExtensions.DaysOfMonth(year, month).Count(date => dayOfWeek.Equals(date.DayOfWeek));
        }

        /// <summary>
        /// 获取一个月内特定DayOfWeek的实例总数。
        /// </summary>
        /// <param name="dayOfWeek">一周中的某一天。</param>
        /// <param name="dateTime">一个月内的日期。</param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, DateTime dateTime)
        {
            return dayOfWeek.TotalInstancesInMonth(dateTime.Year, dateTime.Month);
        }
    }
}