using System;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    /// <see cref=��IComparable t��/>����չ������
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// ���ֵ�Ƿ������Сֵ�����ֵ֮�䡣
        /// </summary>
        /// <param name="value">Ҫ����ֵ</param>
        /// <param name="minInclusiveValue">��С������ֵ</param>
        /// <param name="maxInclusiveValue">��󣨺���ֵ</param>
        public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
        {
            return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
        }
    }
}