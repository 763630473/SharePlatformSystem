using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePlatformSystem.Collections.Extensions
{
    /// <summary> 
    /// “IEnumerable t”的扩展方法。
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 使用每个成员之间的指定分隔符连接已构造的System.String类型集合的成员。
        ///这是string.join（…）的快捷方式。
        /// </summary>
        /// <param name="source">包含要连接的字符串的集合。</param>
        /// <param name="separator">用作分隔符的字符串。只有当值有多个元素时，分隔符才会包含在返回的字符串中。</param>
        /// <returns>由分隔符字符串分隔的值成员组成的字符串。如果值没有成员，则该方法返回System.String.Empty。</returns>
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// 使用每个成员之间的指定分隔符连接集合的成员。
        ///这是string.join（…）的快捷方式。
        /// </summary>
        /// <param name="source">包含要连接的对象的集合。</param>
        /// <param name="separator">用作分隔符的字符串。只有当值有多个元素时，分隔符才会包含在返回的字符串中。</param>
        /// <typeparam name="T">值成员的类型。</typeparam>
        /// <returns>由分隔符字符串分隔的值成员组成的字符串。如果值没有成员，则该方法返回System.String.Empty。</returns>
        public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// 如果给定条件为真，则按给定谓词筛选a。
        /// </summary>
        /// <param name="source">可枚举以应用筛选</param>
        /// <param name="condition">布尔值</param>
        /// <param name="predicate">筛选可枚举的谓词</param>
        /// <returns>已筛选或未筛选基于“条件”可枚举</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }

        /// <summary>
        ///如果给定条件为真，则按给定谓词筛选“IEnumerable t”。
        /// </summary>
        /// <param name="source">可枚举以应用筛选</param>
        /// <param name="condition">布尔值</param>
        /// <param name="predicate">筛选可枚举的谓词</param>
        /// <returns>已筛选或未筛选基于“condition”可枚举"</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }
    }
}
