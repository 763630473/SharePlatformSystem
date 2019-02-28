using System.Collections.Generic;

namespace SharePlatformSystem.Framework.Models.SharePlatformUserConfiguration
{
    public class SharePlatformUserConfigurationDto
    {
        public SharePlatformUserSessionConfigDto Session { get; set; }

        public SharePlatformUserLocalizationConfigDto Localization { get; set; }


        public SharePlatformUserSettingConfigDto Setting { get; set; }

        public SharePlatformUserClockConfigDto Clock { get; set; }
        public SharePlatformUserTimingConfigDto Timing { get; set; }

        public Dictionary<string, object> Custom { get; set; }
    }
}