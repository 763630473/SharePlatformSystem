using System;
using System.Globalization;
using System.Linq;
using System.ComponentModel;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    /// 所有对象的扩展方法。
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 用于简化和美化将对象强制转换为类型。
        /// </summary>
        /// <typeparam name="T">要铸造的类型</typeparam>
        /// <param name="obj">要强制转换的对象</param>
        /// <returns>转换对象</returns>
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// 使用<see cref=“convert.changetype（object，typecode）”/>或<see cref=“enum.parse（type，string）”/>方法将给定对象转换为值或枚举类型。
        /// </summary>
        /// <param name="obj">要强制转换的对象</param>
        /// <typeparam name="T">要铸造的类型</typeparam>
        /// <returns>转换对象</returns>
        public static T To<T>(this object obj)
            where T : struct
        {
            if (typeof(T) == typeof(Guid))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }

            if (typeof(T).IsEnum)
            {
                if (Enum.IsDefined(typeof(T), obj))
                {
                    return (T)Enum.Parse(typeof(T), obj.ToString());
                }
                else
                {
                    throw new ArgumentException($"枚举类型未定义'{obj}'.");
                }
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 检查项目是否在列表中。
        /// </summary>
        /// <param name="item">检查项目</param>
        /// <param name="list">项目清单</param>
        /// <typeparam name="T">项目的类型</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
