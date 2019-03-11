using System;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    /// <see cref=“IComparable t”/>的扩展方法。
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// 检查值是否介于最小值和最大值之间。
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="minInclusiveValue">最小（含）值</param>
        /// <param name="maxInclusiveValue">最大（含）值</param>
        public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
        {
            return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
        }
    }
}