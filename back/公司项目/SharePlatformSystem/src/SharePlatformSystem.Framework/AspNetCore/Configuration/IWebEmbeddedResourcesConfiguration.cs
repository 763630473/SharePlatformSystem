using System.Collections.Generic;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    public interface IWebEmbeddedResourcesConfiguration
    {
        /// <summary>
        /// List of file extensions (without dot) to ignore for embedded resources.
        /// Default extensions: cshtml, config.
        /// </summary>
        HashSet<string> IgnoredFileExtensions { get; }
    }
}