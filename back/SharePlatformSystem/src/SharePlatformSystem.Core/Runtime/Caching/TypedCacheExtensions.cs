using System;
using System.Threading.Tasks;

namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    /// <see cref=��ITypedCache tkey��tValue��/>����չ������
    /// </summary>
    public static class TypedCacheExtensions
    {
        public static TValue Get<TKey, TValue>(this ITypedCache<TKey, TValue> cache, TKey key, Func<TValue> factory)
        {
            return cache.Get(key, k => factory());
        }

        public static Task<TValue> GetAsync<TKey, TValue>(this ITypedCache<TKey, TValue> cache, TKey key, Func<Task<TValue>> factory)
        {
            return cache.GetAsync(key, k => factory());
        }
    }
}