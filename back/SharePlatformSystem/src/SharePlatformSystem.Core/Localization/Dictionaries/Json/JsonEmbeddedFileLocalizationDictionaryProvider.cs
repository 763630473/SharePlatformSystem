using System.Globalization;
using System.Linq;
using System.Reflection;
using SharePlatformSystem.core.Localization.Dictionaries.Xml;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.core.Localization.Dictionaries.Json
{
    /// <summary>
    /// 提供嵌入到“程序集”中的JSON文件的本地化字典。
    /// </summary>
    public class JsonEmbeddedFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly Assembly _assembly;
        private readonly string _rootNamespace;

        /// <summary>
        /// 创建新的“jsonEmbeddedFileLocalizationDictionaryProvider”对象。
        /// </summary>
        /// <param name="assembly">包含嵌入JSON文件的程序集</param>
        /// <param name="rootNamespace">
        /// <para>
        /// 嵌入的JSON字典文件的命名空间
        /// </para>
        /// <para>
        /// 注意：JSON文件夹名与XML文件夹名不同。
        /// </para>
        /// <para>
        /// 必须这样命名：json***和xml****；不要这样命名：****json和****xml
        /// </para>
        /// </param>
        public JsonEmbeddedFileLocalizationDictionaryProvider(Assembly assembly, string rootNamespace)
        {
            _assembly = assembly;
            _rootNamespace = rootNamespace;
        }

        public override void Initialize(string sourceName)
        {
            var allCultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var resourceNames = _assembly.GetManifestResourceNames().Where(resouceName =>
                allCultureInfos.Any(culture => resouceName.EndsWith($"{sourceName}.json", true, null) ||
                                               resouceName.EndsWith($"{sourceName}-{culture.Name}.json", true,
                                                   null))).ToList();
            foreach (var resourceName in resourceNames)
            {
                if (resourceName.StartsWith(_rootNamespace))
                {
                    using (var stream = _assembly.GetManifestResourceStream(resourceName))
                    {
                        var jsonString = Utf8Helper.ReadStringFromStream(stream);

                        var dictionary = CreateJsonLocalizationDictionary(jsonString);
                        if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                        {
                            throw new SharePlatformInitializationException(sourceName + " 源包含多个区域性字典: " + dictionary.CultureInfo.Name);
                        }

                        Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                        if (resourceName.EndsWith(sourceName + ".json"))
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

        protected virtual JsonLocalizationDictionary CreateJsonLocalizationDictionary(string jsonString)
        {
            return JsonLocalizationDictionary.BuildFromJsonString(jsonString);
        }
    }
}