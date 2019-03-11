using System;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 定义设置的范围。
    /// </summary>
    [Flags]
    public enum SettingScopes
    {
        /// <summary>
        /// 表示可为应用程序级别配置/更改的设置。
        /// </summary>
        Application = 1,

        /// <summary>
        /// 表示可以为每个用户配置/更改的设置。
        /// </summary>
        User = 4,

        /// <summary>
        ///表示可以为所有级别配置/更改的设置
        /// </summary>
        All = Application  | User
    }
}