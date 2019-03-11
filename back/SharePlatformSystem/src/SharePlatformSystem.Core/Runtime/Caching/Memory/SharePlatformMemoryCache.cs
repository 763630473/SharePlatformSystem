using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.Runtime.Caching.Memory
{
    /// <summary>
    /// 实现<see cref="ICache"/>以使用<see cref="MemoryCache"/>.
    /// </summary>
    public class SharePlatformMemoryCache : CacheBase
    {
        private MemoryCache _memoryCache;

        /// <summary>
        /// 构造器.
        /// </summary>
        /// <param name="name">缓存的唯一名称</param>
        public SharePlatformMemoryCache(string name)
            : base(name)
        {
            _memoryCache = new MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new SharePlatformException("无法将空值插入缓存！");
            }

            if (absoluteExpireTime != null)
            {
                _memoryCache.Set(key, value, DateTimeOffset.Now.Add(absoluteExpireTime.Value));
            }
            else if (slidingExpireTime != null)
            {
                _memoryCache.Set(key, value, slidingExpireTime.Value);
            }
            else if (DefaultAbsoluteExpireTime != null)
            {
                _memoryCache.Set(key, value, DateTimeOffset.Now.Add(DefaultAbsoluteExpireTime.Value));
            }
            else
            {
                _memoryCache.Set(key, value, DefaultSlidingExpireTime);
            }
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}