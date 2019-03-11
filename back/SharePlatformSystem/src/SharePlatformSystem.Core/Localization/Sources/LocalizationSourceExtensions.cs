using System;
using System.Globalization;

namespace SharePlatformSystem.Core.Localization.Sources
{
    /// <summary>
    /// “ilocalizationsource”的扩展方法。
    /// </summary>
    public static class LocalizationSourceExtensions
    {
        /// <summary>
        /// 通过格式化字符串获取本地化字符串。
        /// </summary>
        /// <param name="source">本地化源</param>
        /// <param name="name">键名</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>格式化和本地化字符串</returns>
        public static string GetString(this ILocalizationSource source, string name, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name), args);
        }

        /// <summary>
        /// 通过格式化字符串来获取给定语言中的本地化字符串。
        /// </summary>
        /// <param name="source">本地化源</param>
        /// <param name="name">键名</param>
        /// <param name="culture">文化</param>
        /// <param name="args">设置参数格式</param>
        /// <returns>格式化和本地化字符串</returns>
        public static string GetString(this ILocalizationSource source, string name, CultureInfo culture, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name, culture), args);
        }
    }
}