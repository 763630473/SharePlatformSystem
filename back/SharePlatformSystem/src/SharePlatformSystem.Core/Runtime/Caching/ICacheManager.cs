using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    ///对象的上层容器。
    ///缓存管理器应作为singleton工作，并跟踪和管理对象。
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// 获取所有缓存。
        /// </summary>
        /// <returns>缓存列表</returns>
        IReadOnlyList<ICache> GetAllCaches();

        /// <summary>
        ///获取<see cref=“icache”/>实例。
        ///如果缓存不存在，可能会创建缓存。
        /// </summary>
        /// <param name="name">
        /// 缓存的唯一和区分大小写的名称。
        /// </param>
        /// <returns>缓存引用</returns>
        [NotNull] ICache GetCache([NotNull] string name);
    }
}
