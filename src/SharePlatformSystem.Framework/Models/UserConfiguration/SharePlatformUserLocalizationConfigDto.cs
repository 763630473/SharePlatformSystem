using System.Collections.Generic;

namespace SharePlatformSystem.Framework.Models.SharePlatformUserConfiguration
{
    public class SharePlatformUserLocalizationConfigDto
    {
        public SharePlatformUserCurrentCultureConfigDto CurrentCulture { get; set; }
   

        public List<SharePlatformLocalizationSourceDto> Sources { get; set; }

        public Dictionary<string, Dictionary<string, string>> Values { get; set; }
    }
}