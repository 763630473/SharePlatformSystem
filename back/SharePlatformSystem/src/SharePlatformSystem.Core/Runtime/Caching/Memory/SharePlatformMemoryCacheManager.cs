using SharePlatformSystem.Dependency;
using SharePlatformSystem.Runtime.Caching.Configuration;
using Castle.Core.Logging;

namespace SharePlatformSystem.Runtime.Caching.Memory
{
    /// <summary>
    /// 实现<see cref="ICacheManager"/>以使用memorycache。 
    /// </summary>
    public class SharePlatformMemoryCacheManager : CacheManagerBase
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// 构造器.
        /// </summary>
        public SharePlatformMemoryCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            Logger = NullLogger.Instance;
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return new SharePlatformMemoryCache(name)
            {
                Logger = Logger
            };
        }

        protected override void DisposeCaches()
        {
            foreach (var cache in Caches.Values)
            {
                cache.Dispose();
            }
        }
    }
}
