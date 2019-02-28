using SharePlatformSystem.core.Configuration.Startup;
using System.Collections.Generic;

namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// Used for localization configurations.
    /// </summary>
    internal class LocalizationConfiguration : ILocalizationConfiguration
    {
        /// <inheritdoc/>
        public ILocalizationSourceList Sources { get; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; }

        /// <inheritdoc/>
        public bool ReturnGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool WrapGivenTextIfNotFound { get; set; }

        /// <inheritdoc/>
        public bool HumanizeTextIfNotFound { get; set; }

        public bool LogWarnMessageIfNotFound { get; set; }

        public LocalizationConfiguration()
        {
            Sources = new LocalizationSourceList();

            IsEnabled = true;
            ReturnGivenTextIfNotFound = true;
            WrapGivenTextIfNotFound = true;
            HumanizeTextIfNotFound = true;
            LogWarnMessageIfNotFound = true;
        }
    }
}
