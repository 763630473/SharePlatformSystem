using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Collections.Extensions
{
    /// <summary>
    /// 集合的扩展方法。
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///检查给定的集合对象是否为空或没有项。
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 如果集合中没有项，则将其添加到集合中。
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">要检查和添加的项</param>
        /// <typeparam name="T">collection中项目的类型</typeparam>
        /// <returns>如果添加则返回true，否则返回false。</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}