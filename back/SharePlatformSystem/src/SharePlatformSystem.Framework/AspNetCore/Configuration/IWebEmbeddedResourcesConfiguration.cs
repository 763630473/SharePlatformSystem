using System.Collections.Generic;

namespace SharePlatformSystem.Framework.AspNetCore.Configuration
{
    public interface IWebEmbeddedResourcesConfiguration
    {
        /// <summary>
        ///要忽略的嵌入资源的文件扩展名列表（不带点）。
        ///默认扩展名：cshtml，config。
        /// </summary>
        HashSet<string> IgnoredFileExtensions { get; }
    }
}