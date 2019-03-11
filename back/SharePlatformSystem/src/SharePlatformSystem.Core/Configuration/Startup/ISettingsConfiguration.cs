using SharePlatformSystem.Collections;
using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// 用于配置设置系统。
    /// </summary>
    public interface ISettingsConfiguration
    {
        /// <summary>
        ///设置提供程序列表。
        /// </summary>
        ITypeList<SettingProvider> Providers { get; }
    }
}
