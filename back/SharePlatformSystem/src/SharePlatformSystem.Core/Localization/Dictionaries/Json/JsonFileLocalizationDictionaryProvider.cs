using System.IO;
using SharePlatformSystem.core.Localization.Dictionaries.Xml;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.core.Localization.Dictionaries.Json
{
    /// <summary>
    /// 提供目录中JSON文件的本地化字典。
    /// </summary>
    public class JsonFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly string _directoryPath;

        /// <summary>
        ///创建新的“JSonfileLocalizationDictionaryProvider”。
        /// </summary>
        /// <param name="directoryPath">包含所有相关XML文件的字典的路径</param>
        public JsonFileLocalizationDictionaryProvider(string directoryPath)
        {
            _directoryPath = directoryPath;
        }
        
        public override void Initialize(string sourceName)
        {
            var fileNames = Directory.GetFiles(_directoryPath, "*.json", SearchOption.TopDirectoryOnly);

            foreach (var fileName in fileNames)
            {
                var dictionary = CreateJsonLocalizationDictionary(fileName);
                if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                {
                    throw new SharePlatformInitializationException(sourceName + " 源包含多个区域性字典: " + dictionary.CultureInfo.Name);
                }

                Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                if (fileName.EndsWith(sourceName + ".json"))
                {
                    if (DefaultDictionary != null)
                    {
                        throw new SharePlatformInitializationException("源只能有一个默认本地化词典: " + sourceName);
                    }

                    DefaultDictionary = dictionary;
                }
            }
        }

        protected virtual JsonLocalizationDictionary CreateJsonLocalizationDictionary(string fileName)
        {
            return JsonLocalizationDictionary.BuildFromFile(fileName);
        }
    }
}