using SharePlatformSystem.Runtime.Caching;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// “icachemanager”获取设置缓存的扩展方法。
    /// </summary>
    public static class CacheManagerSettingExtensions
    {
        /// <summary>
        ///获取应用程序设置缓存。
        /// </summary>
        public static ITypedCache<string, Dictionary<string, SettingInfo>> GetApplicationSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<string, Dictionary<string, SettingInfo>>(SharePlatformCacheNames.ApplicationSettings);
        }

        /// <summary>
        /// 获取用户设置缓存。
        /// </summary>
        public static ITypedCache<string, Dictionary<string, SettingInfo>> GetUserSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<string, Dictionary<string, SettingInfo>>(SharePlatformCacheNames.UserSettings);
        }
    }
}
