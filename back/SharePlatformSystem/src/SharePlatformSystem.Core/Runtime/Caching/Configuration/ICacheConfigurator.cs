using System;

namespace SharePlatformSystem.Runtime.Caching.Configuration
{
    /// <summary>
    /// 已注册的缓存配置程序。
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        ///缓存的名称。
        ///如果此配置器配置所有缓存，则为空。
        /// </summary>
        string CacheName { get; }

        /// <summary>
        ///配置操作。在创建缓存后立即调用。
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}