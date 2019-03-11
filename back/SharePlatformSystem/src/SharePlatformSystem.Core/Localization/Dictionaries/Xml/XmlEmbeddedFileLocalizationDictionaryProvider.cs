using SharePlatformSystem.Core.Exceptions;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SharePlatformSystem.core.Localization.Dictionaries.Xml
{
    /// <summary>
    /// 提供嵌入到“assembly”中的XML文件的本地化字典。
    /// </summary>
    public class XmlEmbeddedFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly Assembly _assembly;
        private readonly string _rootNamespace;

        /// <summary>
        /// 创建新的“xmleEmbeddedFileLocalizationDictionaryProvider”对象。
        /// </summary>
        /// <param name="assembly">包含嵌入XML文件的程序集</param>
        /// <param name="rootNamespace">嵌入的XML字典文件的命名空间</param>
        public XmlEmbeddedFileLocalizationDictionaryProvider(Assembly assembly, string rootNamespace)
        {
            _assembly = assembly;
            _rootNamespace = rootNamespace;
        }

        public override void Initialize(string sourceName)
        {
            var allCultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var resourceNames = _assembly.GetManifestResourceNames().Where(resouceName =>
                allCultureInfos.Any(culture => resouceName.EndsWith($"{sourceName}.xml", true, null) ||
                                               resouceName.EndsWith($"{sourceName}-{culture.Name}.xml", true,
                                                   null))).ToList();
            foreach (var resourceName in resourceNames)
            {
                if (resourceName.StartsWith(_rootNamespace))
                {
                    using (var stream = _assembly.GetManifestResourceStream(resourceName))
                    {
                        var xmlString = Utf8Helper.ReadStringFromStream(stream);

                        var dictionary = CreateXmlLocalizationDictionary(xmlString);
                        if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                        {
                            throw new SharePlatformInitializationException(sourceName + " 源包含多个区域性字典: " + dictionary.CultureInfo.Name);
                        }

                        Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                        if (resourceName.EndsWith(sourceName + ".xml"))
                        {
                            if (DefaultDictionary != null)
                            {
                                throw new SharePlatformInitializationException("源只能有一个默认本地化词典: " + sourceName);
                            }

                            DefaultDictionary = dictionary;
                        }
                    }
                }
            }
        }

        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string xmlString)
        {
            return XmlLocalizationDictionary.BuildFomXmlString(xmlString);
        }
    }
}