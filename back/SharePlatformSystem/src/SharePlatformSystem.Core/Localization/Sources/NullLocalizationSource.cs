using System.Collections.Generic;
using System.Globalization;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Localization.Sources
{
    /// <summary>
    /// “ilocalizationsource”的对象模式为空。
    /// </summary>
    public class NullLocalizationSource : ILocalizationSource
    {
        /// <summary>
        ///单例实例。
        /// </summary>
        public static NullLocalizationSource Instance { get; } = new NullLocalizationSource();

        public string Name { get { return null; } }

        private readonly IReadOnlyList<LocalizedString> _emptyStringArray = new LocalizedString[0];

        private NullLocalizationSource()
        {

        }

        public void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {

        }

        public string GetString(string name)
        {
            return name;
        }

        public string GetString(string name, CultureInfo culture)
        {
            return name;
        }

        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return null;
        }

        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            return null;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return _emptyStringArray;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            return _emptyStringArray;
        }
    }
}
