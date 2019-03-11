using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;

namespace SharePlatformSystem.core.Localization.Dictionaries
{
    /// <summary>
    /// 表示“ilocalizationDictionary”接口的简单实现。
    /// </summary>
    public class LocalizationDictionary : ILocalizationDictionary, IEnumerable<LocalizedString>
    {
        /// <inheritdoc/>
        public CultureInfo CultureInfo { get; private set; }

        /// <inheritdoc/>
        public virtual string this[string name]
        {
            get
            {
                var localizedString = GetOrNull(name);
                return localizedString?.Value;
            }
            set => _dictionary[name] = new LocalizedString(name, value, CultureInfo);
        }

        private readonly Dictionary<string, LocalizedString> _dictionary;

        /// <summary>
        /// 创建一个新的<see cref="LocalizationDictionary"/> 对象.
        /// </summary>
        /// <param name="cultureInfo">Culture of the dictionary</param>
        public LocalizationDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
            _dictionary = new Dictionary<string, LocalizedString>();
        }

        /// <inheritdoc/>
        public virtual LocalizedString GetOrNull(string name)
        {
            return _dictionary.TryGetValue(name, out var localizedString) ? localizedString : null;
        }

        public virtual IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return _dictionary.Values.ToImmutableList();
        }

        public virtual IEnumerator<LocalizedString> GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        protected bool Contains(string name)
        {
            return _dictionary.ContainsKey(name);
        }
    }
}