using SharePlatformSystem.core.Localization.Dictionaries;

namespace SharePlatformSystem.Core.Localization.Sources
{
    /// <summary>
    /// 用于存储本地化源扩展信息。
    /// </summary>
    public class LocalizationSourceExtensionInfo
    {
        /// <summary>
        /// 源名称。
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        ///扩展字典。
        /// </summary>
        public ILocalizationDictionaryProvider DictionaryProvider { get; private set; }

        /// <summary>
        ///创建新的“LocalizationSourceExtensionInfo”对象。
        /// </summary>
        /// <param name="sourceName">源名称</param>
        /// <param name="dictionaryProvider">扩展字典</param>
        public LocalizationSourceExtensionInfo(string sourceName, ILocalizationDictionaryProvider dictionaryProvider)
        {
            SourceName = sourceName;
            DictionaryProvider = dictionaryProvider;
        }
    }
}