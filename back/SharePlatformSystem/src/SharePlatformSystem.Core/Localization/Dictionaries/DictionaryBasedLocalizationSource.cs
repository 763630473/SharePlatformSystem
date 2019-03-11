using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using Castle.Core.Logging;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.core.Localization.Dictionaries
{
    /// <summary>
    ///此类用于生成本地化源
    ///它使用基于内存的字典来查找字符串。
    /// </summary>
    public class DictionaryBasedLocalizationSource : IDictionaryBasedLocalizationSource
    {
        /// <summary>
        /// 源的唯一名称。
        /// </summary>
        public string Name { get; }

        public ILocalizationDictionaryProvider DictionaryProvider { get; }

        protected ILocalizationConfiguration LocalizationConfiguration { get; private set; }

        private ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dictionaryProvider"></param>
        public DictionaryBasedLocalizationSource(string name, ILocalizationDictionaryProvider dictionaryProvider)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNull(dictionaryProvider, nameof(dictionaryProvider));

            Name = name;
            DictionaryProvider = dictionaryProvider;
        }

        public virtual void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            LocalizationConfiguration = configuration;

            _logger = iocResolver.IsRegistered(typeof(ILoggerFactory))
                ? iocResolver.Resolve<ILoggerFactory>().Create(typeof(DictionaryBasedLocalizationSource))
                : NullLogger.Instance;

            DictionaryProvider.Initialize(Name);
        }

        public string GetString(string name)
        {
            return GetString(name, CultureInfo.CurrentUICulture);
        }

        public string GetString(string name, CultureInfo culture)
        {
            var value = GetStringOrNull(name, culture);

            if (value == null)
            {
                return ReturnGivenNameOrThrowException(name, culture);
            }

            return value;
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return GetStringOrNull(name, CultureInfo.CurrentUICulture, tryDefaults);
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            var cultureName = culture.Name;
            var dictionaries = DictionaryProvider.Dictionaries;

            //试着从原始字典（带国家代码）
            ILocalizationDictionary originalDictionary;
            if (dictionaries.TryGetValue(cultureName, out originalDictionary))
            {
                var strOriginal = originalDictionary.GetOrNull(name);
                if (strOriginal != null)
                {
                    return strOriginal.Value;
                }
            }

            if (!tryDefaults)
            {
                return null;
            }

            //试着从同一语言字典（没有国家代码）
            if (cultureName.Contains("-")) //Example: "tr-TR" (length=5)
            {
                ILocalizationDictionary langDictionary;
                if (dictionaries.TryGetValue(GetBaseCultureName(cultureName), out langDictionary))
                {
                    var strLang = langDictionary.GetOrNull(name);
                    if (strLang != null)
                    {
                        return strLang.Value;
                    }
                }
            }

            //尝试从默认语言获取
            var defaultDictionary = DictionaryProvider.DefaultDictionary;
            if (defaultDictionary == null)
            {
                return null;
            }

            var strDefault = defaultDictionary.GetOrNull(name);
            if (strDefault == null)
            {
                return null;
            }

            return strDefault.Value;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return GetAllStrings(CultureInfo.CurrentUICulture, includeDefaults);
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            //TODO:可以优化（例如：如果它已经是默认字典，则跳过覆盖）

            var dictionaries = DictionaryProvider.Dictionaries;

            //创建要生成的临时字典
            var allStrings = new Dictionary<string, LocalizedString>();

            if (includeDefaults)
            {
                //填充默认字典中的所有字符串
                var defaultDictionary = DictionaryProvider.DefaultDictionary;
                if (defaultDictionary != null)
                {
                    foreach (var defaultDictString in defaultDictionary.GetAllStrings())
                    {
                        allStrings[defaultDictString.Name] = defaultDictString;
                    }
                }

                //基于国家文化覆盖语言中的所有字符串
                if (culture.Name.Contains("-"))
                {
                    ILocalizationDictionary langDictionary;
                    if (dictionaries.TryGetValue(GetBaseCultureName(culture.Name), out langDictionary))
                    {
                        foreach (var langString in langDictionary.GetAllStrings())
                        {
                            allStrings[langString.Name] = langString;
                        }
                    }
                }
            }

            //覆盖原始字典中的所有字符串
            ILocalizationDictionary originalDictionary;
            if (dictionaries.TryGetValue(culture.Name, out originalDictionary))
            {
                foreach (var originalLangString in originalDictionary.GetAllStrings())
                {
                    allStrings[originalLangString.Name] = originalLangString;
                }
            }

            return allStrings.Values.ToImmutableList();
        }

        /// <summary>
        /// 使用给定的字典扩展源。
        /// </summary>
        /// <param name="dictionary">扩展源的字典</param>
        public virtual void Extend(ILocalizationDictionary dictionary)
        {
            DictionaryProvider.Extend(dictionary);
        }

        protected virtual string ReturnGivenNameOrThrowException(string name, CultureInfo culture)
        {
            return LocalizationSourceHelper.ReturnGivenNameOrThrowException(
                LocalizationConfiguration,
                Name,
                name,
                culture,
                _logger
            );
        }

        private static string GetBaseCultureName(string cultureName)
        {
            return cultureName.Contains("-")
                ? cultureName.Left(cultureName.IndexOf("-", StringComparison.Ordinal))
                : cultureName;
        }
    }
}
