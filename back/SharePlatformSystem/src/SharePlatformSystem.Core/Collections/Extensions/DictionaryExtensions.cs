using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Collections.Extensions
{
    /// <summary>
    /// 字典的扩展方法。
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 此方法用于尝试在字典中获取值（如果存在）。
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="dictionary">集合对象</param>
        /// <param name="key">键值</param>
        /// <param name="value">键的值（如果键不存在，则为默认值）</param>
        /// <returns>如果字典中确实存在键，则为true</returns>
        internal static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            object valueObj;
            if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
            {
                value = (T)valueObj;
                return true;
            }

            value = default(T);
            return false;
        }

        /// <summary>
        ///从字典中获取具有给定键的值。如果找不到，则返回默认值。
        /// </summary>
        /// <param name="dictionary">要检查和获取的词典</param>
        /// <param name="key">找到值的键</param>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <returns>值（如果找到），默认值（如果找不到）。</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default(TValue);
        }

        /// <summary>
        /// 从字典中获取具有给定键的值。如果找不到，则返回默认值。
        /// </summary>
        /// <param name="dictionary">要检查和获取的词典</param>
        /// <param name="key">找到值的键</param>
        /// <param name="factory">如果在字典中找不到值，则用于创建值的工厂方法。</param>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <returns>值（如果找到），默认值（如果找不到）。</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            TValue obj;
            if (dictionary.TryGetValue(key, out obj))
            {
                return obj;
            }

            return dictionary[key] = factory(key);
        }

        /// <summary>
        /// 从字典中获取具有给定键的值。如果找不到，则返回默认值。
        /// </summary>
        /// <param name="dictionary">要检查和获取的词典</param>
        /// <param name="key">找到值的键</param>
        /// <param name="factory">如果在字典中找不到值，则用于创建值的工厂方法。</param>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <returns>值（如果找到），默认值（如果找不到）。</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            return dictionary.GetOrAdd(key, k => factory());
        }
    }
}