using System.Collections.Generic;
using System.Globalization;

namespace SharePlatformSystem.core.Localization.Dictionaries
{
    /// <summary>
    /// 表示用于查找本地化字符串的字典。
    /// </summary>
    public interface ILocalizationDictionary
    {
        /// <summary>
        /// 字典的文化。
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        ///获取/设置具有给定名称（键）的此字典的字符串。
        /// </summary>
        /// <param name="name">获取/设置名称</param>
        string this[string name] { get; set; }

        /// <summary>
        /// 获取给定的“name”的“localizedstring”。
        /// </summary>
        /// <param name="name">获取本地化字符串的名称（键）</param>
        /// <returns>本地化字符串，如果在此词典中找不到，则为空。</returns>
        LocalizedString GetOrNull(string name);

        /// <summary>
        /// 获取此字典中所有字符串的列表。
        /// </summary>
        /// <returns>List of all <see cref="LocalizedString"/> 对象</returns>
        IReadOnlyList<LocalizedString> GetAllStrings();
    }
}