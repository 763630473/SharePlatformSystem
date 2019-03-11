using SharePlatformSystem.Core.Localization.Sources;

namespace SharePlatformSystem.core.Localization.Dictionaries
{
    /// <summary>
    /// 基于字典的本地化源的接口。
    /// </summary>
    public interface IDictionaryBasedLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// 获取字典提供程序。
        /// </summary>
        ILocalizationDictionaryProvider DictionaryProvider { get; }

        /// <summary>
        /// 使用给定的字典扩展源。
        /// </summary>
        /// <param name="dictionary">扩展源的字典</param>
        void Extend(ILocalizationDictionary dictionary);
    }
}