using System;
using System.Collections.Generic;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Runtime.Caching.Configuration
{
    /// <summary>
    ///用于配置缓存系统。
    /// </summary>
    public interface ICachingConfiguration
    {
        /// <summary>
        /// 获取SharePlatform配置对象。
        /// </summary>
        ISharePlatformStartupConfiguration SharePlatformConfiguration { get; }

        /// <summary>
        /// 所有已注册配置程序的列表。
        /// </summary>
        IReadOnlyList<ICacheConfigurator> Configurators { get; }

        /// <summary>
        /// 用于配置所有缓存。
        /// </summary>
        /// <param name="initAction">
        ///配置缓存的操作
        ///此操作将在创建后为每个缓存调用。
        /// </param>
        void ConfigureAll(Action<ICache> initAction);

        /// <summary>
        /// 用于配置特定缓存。
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="initAction">
        ///配置缓存的操作。
        ///在创建缓存后立即调用此操作。
        /// </param>
        void Configure(string cacheName, Action<ICache> initAction);
    }
}
