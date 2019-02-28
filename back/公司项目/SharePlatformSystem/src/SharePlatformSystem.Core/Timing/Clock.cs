using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Timing
{
    /// <summary>
    /// 用于执行一些常见的日期时间操作。
    /// </summary>
    public static class Clock
    {
        /// <summary>
        /// 此对象用于执行所有“时钟”操作。
        ///默认值：“unspecifiedClockProvider”。
        /// </summary>
        public static IClockProvider Provider
        {
            get { return _provider; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Can not set Clock.Provider to null!");
                }

                _provider = value;
            }
        }

        private static IClockProvider _provider;

        static Clock()
        {
            Provider = ClockProviders.Unspecified;
        }

        /// <summary>
        ///现在使用当前的“Provider”。
        /// </summary>
        public static DateTime Now => Provider.Now;

        public static DateTimeKind Kind => Provider.Kind;

        /// <summary>
        /// 如果支持多个时区，则返回true；如果不支持，则返回false。
        /// </summary>
        public static bool SupportsMultipleTimezone => Provider.SupportsMultipleTimezone;

        /// <summary>
        /// 使用当前规范化给定的datetime。
        /// </summary>
        /// <param name="dateTime">要规范化的日期时间。</param>
        /// <returns>标准化日期时间</returns>
        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}