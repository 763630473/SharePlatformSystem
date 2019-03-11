using System.Collections.Generic;
using SharePlatformSystem.Core.Localization.Sources;

namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// 定义用于存储“ilocalizationSource”对象的专用列表。
    /// </summary>
    public interface ILocalizationSourceList : IList<ILocalizationSource>
    {
        /// <summary>
        /// 基于词典的本地化源的扩展。
        /// </summary>
        IList<LocalizationSourceExtensionInfo> Extensions { get; }
    }
}