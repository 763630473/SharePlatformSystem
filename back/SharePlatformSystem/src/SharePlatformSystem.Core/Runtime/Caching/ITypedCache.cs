using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    ///以类型化方式处理缓存的接口。
    ///使用<see cref=“cacheextensions.astyped tkey，tValue”/>方法
    ///将a<see cref=“icache”/>转换为此接口。
    /// </summary>
    /// <typeparam name="TKey">缓存项的键类型</typeparam>
    /// <typeparam name="TValue">缓存项的值类型</typeparam>
    public interface ITypedCache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// 缓存的唯一名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 缓存项的默认滑动过期时间。
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// 获取内部缓存。
        /// </summary>
        ICache InternalCache { get; }

        /// <summary>
        /// 从缓存中获取项。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>Cached item</returns>
        TValue Get(TKey key, Func<TKey, TValue> factory);

        /// <summary>
        /// 从缓存获取项。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>Cached items</returns>
        TValue[] Get(TKey[] keys, Func<TKey, TValue> factory);

        /// <summary>
        /// 从缓存中获取项。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存项</returns>
        Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// 从缓存获取项。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存项</returns>
        Task<TValue[]> GetAsync(TKey[] keys, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// 从缓存中获取项，如果未找到，则为空。
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>缓存项，如果找不到则为空</returns>
        TValue GetOrDefault(TKey key);

        /// <summary>
        /// 从缓存获取项。对于每个未找到的键，都会返回一个空值。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>缓存项</returns>
        TValue[] GetOrDefault(TKey[] keys);

        /// <summary>
        /// 从缓存中获取项，如果未找到，则为空。
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>缓存项，如果找不到则为空</returns>
        Task<TValue> GetOrDefaultAsync(TKey key);

        /// <summary>
        ///从缓存获取项。对于每个未找到的键，都会返回一个空值。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>缓存项</returns>
        Task<TValue[]> GetOrDefaultAsync(TKey[] keys);

        /// <summary>
        /// 通过键保存/重写缓存中的项。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// 按对保存/重写缓存中的项。
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        void Set(KeyValuePair<TKey, TValue>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// 通过键保存/重写缓存中的项。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// 按对保存/重写缓存中的项。
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        Task SetAsync(KeyValuePair<TKey, TValue>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// 按缓存项的键删除缓存项（如果缓存中不存在给定的键，则不执行任何操作）。
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(TKey key);

        /// <summary>
        /// 按键删除缓存项。
        /// </summary>
        /// <param name="keys">Keys</param>
        void Remove(TKey[] keys);

        /// <summary>
        ///按缓存项的键删除缓存项（如果缓存中不存在给定的键，则不执行任何操作）。
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(TKey key);

        /// <summary>
        /// 按键删除缓存项。
        /// </summary>
        /// <param name="keys">Keys</param>
        Task RemoveAsync(TKey[] keys);

        /// <summary>
        ///清除此缓存中的所有项目。
        /// </summary>
        void Clear();

        /// <summary>
        /// 清除此缓存中的所有项目。
        /// </summary>
        Task ClearAsync();
    }
}