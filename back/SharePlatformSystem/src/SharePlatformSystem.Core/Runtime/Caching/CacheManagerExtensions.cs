namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    ///<see cref="ICacheManager"/>的扩展方法。
    /// </summary>
    public static class CacheManagerExtensions
    {
        public static ITypedCache<TKey, TValue> GetCache<TKey, TValue>(this ICacheManager cacheManager, string name)
        {
            return cacheManager.GetCache(name).AsTyped<TKey, TValue>();
        }
    }
}