using JetBrains.Annotations;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Resources.Embedded
{
    /// <summary>
    /// 提供基础结构以使用嵌入到程序集中的任何类型的资源（文件）。
    /// </summary>
    public interface IEmbeddedResourceManager
    {
        /// <summary>
        ///用于获取嵌入的资源文件。
        ///如果找不到资源，可以返回空值！
        /// </summary>
        /// <param name="fullResourcePath">资源的完整路径</param>
        /// <returns>The resource</returns>
        [CanBeNull]
        EmbeddedResourceItem GetResource([NotNull] string fullResourcePath);

        /// <summary>
        /// 用于获取嵌入资源文件的列表。
        /// </summary>
        /// <param name="fullResourcePath">资源的完整路径</param>
        /// <returns>资源列表</returns>
        IEnumerable<EmbeddedResourceItem> GetResources(string fullResourcePath);
    }
}