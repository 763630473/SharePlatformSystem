using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    /// 定义可以按键存储和获取项目的缓存。
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// 缓存的唯一名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        ///缓存项的默认滑动过期时间。
        ///默认值：60分钟（1小时）。
        ///可以通过配置更改。
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        ///缓存项的默认绝对过期时间。
        ///默认值：空（未使用）。
        /// </summary>
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        /// <summary>
        ///从缓存中获取项。
        ///此方法隐藏缓存提供程序失败（并记录它们），
        ///如果缓存提供程序失败，则使用工厂方法获取对象。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存项</returns>
        object Get(string key, Func<string, object> factory);

        /// <summary>
        ///从缓存中获取项。
        ///此方法隐藏缓存提供程序失败（并记录它们），
        ///如果缓存提供程序失败，则使用工厂方法获取对象。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存项</returns>
        object[] Get(string[] keys, Func<string, object> factory);

        /// <summary>
        ///从缓存中获取项。
        ///此方法隐藏缓存提供程序失败（并记录它们），
        ///如果缓存提供程序失败，则使用工厂方法获取对象。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存项</returns>
        Task<object> GetAsync(string key, Func<string, Task<object>> factory);

        /// <summary>
        ///从缓存中获取项。
        ///此方法隐藏缓存提供程序失败（并记录它们），
        ///如果缓存提供程序失败，则使用工厂方法获取对象。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存项</returns>
        Task<object[]> GetAsync(string[] keys, Func<string, Task<object>> factory);

        /// <summary>
        /// 从缓存中获取项，如果未找到，则为空。
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>缓存项，如果找不到则为空</returns>
        object GetOrDefault(string key);

        /// <summary>
        /// 从缓存获取项。对于每个未找到的键，都会返回一个空值。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>缓存项</returns>
        object[] GetOrDefault(string[] keys);

        /// <summary>
        /// 从缓存中获取项，如果未找到，则为空。
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>缓存项，如果找不到则为空</returns>
        Task<object> GetOrDefaultAsync(string key);

        /// <summary>
        /// 从缓存获取项。对于每个未找到的键，都会返回一个空值。
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>缓存项</returns>
        Task<object[]> GetOrDefaultAsync(string[] keys);

        /// <summary>
        ///用键保存/重写缓存中的项。
        ///最多使用一个过期时间（<paramref name=“slidingExpireTime”/>或<paramref name=“absoluteExpireTime”/>）。
        ///如果没有指定，则
        ///<see cref=“defaultAbsoluteExpireTime”/>如果不为空，将使用它。Othewise，<see cref=“defaultslidingExpireTime”/>
        ///将被使用。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        ///按对保存/重写缓存中的项。
        ///最多使用一个过期时间（<paramref name=“slidingExpireTime”/>或<paramref name=“absoluteExpireTime”/>）。
        ///如果没有指定，则
        ///<see cref=“defaultAbsoluteExpireTime”/>如果不为空，将使用它。Othewise，<see cref=“defaultslidingExpireTime”/>
        ///将被使用。
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        void Set(KeyValuePair<string, object>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        ///用键保存/重写缓存中的项。
        ///最多使用一个过期时间（<paramref name=“slidingExpireTime”/>或<paramref name=“absoluteExpireTime”/>）。
        ///如果没有指定，则
        ///<see cref=“defaultAbsoluteExpireTime”/>如果不为空，将使用它。Othewise，<see cref=“defaultslidingExpireTime”/>

        ///将被使用。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// 按对保存/重写缓存中的项。
        ///最多使用一个过期时间（<paramref name=“slidingExpireTime”/>或<paramref name=“absoluteExpireTime”/>）。
        /// 如果没有指定，则
        ///<see cref=“defaultAbsoluteExpireTime”/>如果不为空，将使用它。Othewise，<see cref=“defaultslidingExpireTime”/>
        ///将被使用。
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        Task SetAsync(KeyValuePair<string, object>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        ///按缓存项的键删除缓存项（如果缓存中不存在给定的键，则不执行任何操作）。
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(string key);

        /// <summary>
        /// 按键删除缓存项。
        /// </summary>
        /// <param name="keys">Keys</param>
        void Remove(string[] keys);

        /// <summary>
        /// 按缓存项的键删除缓存项（如果缓存中不存在给定的键，则不执行任何操作）。
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(string key);

        /// <summary>
        ///按键移除缓存项。
        /// </summary>
        /// <param name="keys">Keys</param>
        Task RemoveAsync(string[] keys);

        /// <summary>
        ///清除此缓存中的所有项目。
        /// </summary>
        void Clear();

        /// <summary>
        ///清除此缓存中的所有项。
        /// </summary>
        Task ClearAsync();
    }
}
