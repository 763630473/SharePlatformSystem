using System.Collections.Generic;

namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// 用于本地化配置。
    /// </summary>
    public interface ILocalizationConfiguration
    {

        /// <summary>
        /// 本地化源列表。
        /// </summary>
        ILocalizationSourceList Sources { get; }

        /// <summary>
        ///用于启用/禁用本地化系统。
        ///默认值：真。
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        ///如果设置为true，则返回给定的文本（名称）
        ///如果在本地化源中找不到。如果
        ///本地化源中未定义给定名称。
        ///同时写入警告日志。
        ///默认值：真。
        /// </summary>
        bool ReturnGivenTextIfNotFound { get; set; }

        /// <summary>
        ///返回给定文本，用[和]字符换行
        ///如果在本地化源中找不到。
        ///只有当“returngiventextifnotfound”为真时才考虑此问题。
        ///默认值：真。
        /// </summary>
        bool WrapGivenTextIfNotFound { get; set; }

        /// <summary>
        ///返回给定文本，方法是将字符串从“pascalcase”转换为“sentense case”
        ///如果在本地化源中找不到。
        ///只有当“returngiventextifnotfound”为真时才考虑此问题。
        ///默认值：真。
        /// </summary>
        bool HumanizeTextIfNotFound { get; set; }

        /// <summary>
        ///如果在本地化源中找不到给定的文本，则写入（或不写入）警告日志。
        ///默认值：真。
        /// </summary>
        bool LogWarnMessageIfNotFound { get; set; }
    }
}
