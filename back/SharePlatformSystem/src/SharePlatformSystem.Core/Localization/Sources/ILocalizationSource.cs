using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Dependency;
using System.Collections.Generic;
using System.Globalization;

namespace SharePlatformSystem.Core.Localization.Sources
{
    /// <summary>
    ///本地化源用于获取本地化字符串。
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// 源的唯一名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 此方法在第一次使用之前由SharePlatform调用。
        /// </summary>
        void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver);

        /// <summary>
        ///获取当前语言中给定名称的本地化字符串。
        ///如果在当前区域性中找不到，则返回默认语言。
        /// </summary>
        /// <param name="name">键名</param>
        /// <returns>本地化字符串</returns>
        string GetString(string name);

        /// <summary>
        ///获取给定名称和指定区域性的本地化字符串。
        ///如果在给定的区域性中找不到，则返回默认语言。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">文化信息</param>
        /// <returns>本地化字符串</returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        ///获取当前语言中给定名称的本地化字符串。
        ///如果找不到，则返回空值。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="tryDefaults">
        /// 如果在当前区域性中找不到，则返回默认语言。
        /// </param>
        /// <returns>本地化字符串</returns>
        string GetStringOrNull(string name, bool tryDefaults = true);

        /// <summary>
        /// 获取给定名称和指定区域性的本地化字符串。
        ///如果找不到，则返回空值。
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="culture">文化信息</param>
        /// <param name="tryDefaults">
        ///true:如果在当前区域性中找不到，则回退到默认语言。
        /// </param>
        /// <returns>本地化字符串</returns>
        string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// 获取当前语言中的所有字符串。
        /// </summary>
        /// <param name="includeDefaults">
        ///true:如果在当前区域性中找不到，则回退到默认语言文本。
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true);

        /// <summary>
        ///获取指定区域性中的所有字符串。
        /// </summary>
        /// <param name="culture">文化信息</param>
        /// <param name="includeDefaults">
        ///true:如果在当前区域性中找不到，则回退到默认语言文本。
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true);
    }
}