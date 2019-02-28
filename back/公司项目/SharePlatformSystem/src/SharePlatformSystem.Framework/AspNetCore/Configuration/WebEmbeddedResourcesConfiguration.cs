using System.Collections.Generic;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    internal class WebEmbeddedResourcesConfiguration : IWebEmbeddedResourcesConfiguration
    {
        public HashSet<string> IgnoredFileExtensions { get; }

        public WebEmbeddedResourcesConfiguration()
        {
            IgnoredFileExtensions = new HashSet<string>
            {
                "cshtml",
                "config"
            };
        }
    }
}