using System;

namespace SharePlatformSystem.Core.Timing.Timezone
{
    /// <summary>
    /// 时区转换器接口
    /// </summary>
    public interface ITimeZoneConverter
    {
        /// <summary>
        ///将给定日期转换为用户的时区。
        ///如果未指定时区设置，则返回给定日期。
        /// </summary>
        /// <param name="date">要转换的基准日期</param>
        /// <param name="userId">要转换日期的用户ID</param>
        /// <returns></returns>
        DateTime? Convert(DateTime? date, string userId);

        /// <summary>
        ///将给定日期转换为应用程序的时区。
        ///如果未指定时区设置，则返回给定日期。
        /// </summary>
        /// <param name="date">要转换的基准日期</param>
        /// <returns></returns>
        DateTime? Convert(DateTime? date);
    }
}
