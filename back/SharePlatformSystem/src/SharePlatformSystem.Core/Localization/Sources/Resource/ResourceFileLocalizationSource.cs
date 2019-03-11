using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using Castle.Core.Logging;
using System.Collections;
using System.Collections.Immutable;
using System.Linq;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.core.Localization;

namespace SharePlatformSystem.Core.Localization.Sources.Resource
{
    /// <summary>
    /// 此类用于简化创建本地化源
    ///使用资源A文件。
    /// </summary>
    public class ResourceFileLocalizationSource : ILocalizationSource, ISingletonDependency
    {
        /// <summary>
        /// 源的唯一名称。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 引用与此本地化源相关的“ResourceManager”对象。
        /// </summary>
        public ResourceManager ResourceManager { get; }

        private ILogger _logger;
        private ILocalizationConfiguration _configuration;

        /// <param name="name">源的唯一名称</param>
        /// <param name="resourceManager">引用与此本地化源相关的“ResourceManager”对象</param>
        public ResourceFileLocalizationSource(string name, ResourceManager resourceManager)
        {
            Name = name;
            ResourceManager = resourceManager;
        }

        /// <summary>
        /// 此方法在第一次使用之前由SharePlatform调用。
        /// </summary>
        public virtual void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            _configuration = configuration;

            _logger = iocResolver.IsRegistered(typeof(ILoggerFactory))
                ? iocResolver.Resolve<ILoggerFactory>().Create(typeof(ResourceFileLocalizationSource))
                : NullLogger.Instance;
        }

        public virtual string GetString(string name)
        {
            var value = GetStringOrNull(name);
            if (value == null)
            {
                return ReturnGivenNameOrThrowException(name, CultureInfo.CurrentUICulture);
            }

            return value;
        }

        public virtual string GetString(string name, CultureInfo culture)
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
            //WARN: Trydefaults未实现！
            return ResourceManager.GetString(name);
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            //WARN:Trydefaults未实现！
            return ResourceManager.GetString(name, culture);
        }

        /// <summary>
        ///获取当前语言中的所有字符串。
        /// </summary>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return GetAllStrings(CultureInfo.CurrentUICulture, includeDefaults);
        }

        /// <summary>
        ///获取指定区域性中的所有字符串。
        /// </summary>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            return ResourceManager
                .GetResourceSet(culture, true, includeDefaults)
                .Cast<DictionaryEntry>()
                .Select(entry => new LocalizedString(entry.Key.ToString(), entry.Value.ToString(), culture))
                .ToImmutableList();
        }

        protected virtual string ReturnGivenNameOrThrowException(string name, CultureInfo culture)
        {
            return LocalizationSourceHelper.ReturnGivenNameOrThrowException(
                _configuration,
                Name,
                name,
                culture,
                _logger
            );
        }
    }
}
