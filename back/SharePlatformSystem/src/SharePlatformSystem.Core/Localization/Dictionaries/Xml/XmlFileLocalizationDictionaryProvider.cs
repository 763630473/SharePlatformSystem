using SharePlatformSystem.Core.Exceptions;
using System.IO;

namespace SharePlatformSystem.core.Localization.Dictionaries.Xml
{
    /// <summary>
    ///提供来自目录中XML文件的本地化字典。
    /// </summary>
    public class XmlFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly string _directoryPath;

        /// <summary>
        /// 创建新的“xmlfilelocalizationDictionaryProvider”。
        /// </summary>
        /// <param name="directoryPath">包含所有相关XML文件的字典的路径</param>
        public XmlFileLocalizationDictionaryProvider(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public override void Initialize(string sourceName)
        {
            var fileNames = Directory.GetFiles(_directoryPath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var fileName in fileNames)
            {
                var dictionary = CreateXmlLocalizationDictionary(fileName);
                if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                {
                    throw new SharePlatformInitializationException(sourceName + " 源包含多个区域性字典: " + dictionary.CultureInfo.Name);
                }

                Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                if (fileName.EndsWith(sourceName + ".xml"))
                {
                    if (DefaultDictionary != null)
                    {
                        throw new SharePlatformInitializationException("源只能有一个默认本地化词典: " + sourceName);                        
                    }

                    DefaultDictionary = dictionary;
                }
            }
        }

        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string fileName)
        {
            return XmlLocalizationDictionary.BuildFomFile(fileName);
        }
    }
}