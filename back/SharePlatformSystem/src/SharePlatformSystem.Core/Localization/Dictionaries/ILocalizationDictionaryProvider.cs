using System.Collections.Generic;

namespace SharePlatformSystem.core.Localization.Dictionaries
{
    /// <summary>
    ///用于获取本地化字典（“ilocalizationDictionary）
    ///有关“idictionaryBasedLocalizationSource”
    /// </summary>
    public interface ILocalizationDictionaryProvider
    {
        ILocalizationDictionary DefaultDictionary { get; }

        IDictionary<string, ILocalizationDictionary> Dictionaries { get; }

        void Initialize(string sourceName);
        
        void Extend(ILocalizationDictionary dictionary);
    }
}