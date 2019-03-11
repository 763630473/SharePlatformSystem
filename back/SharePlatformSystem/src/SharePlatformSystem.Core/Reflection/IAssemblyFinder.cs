using System.Collections.Generic;
using System.Reflection;

namespace SharePlatformSystem.Core.Reflection
{
    /// <summary>
    /// 此接口用于获取应用程序中的程序集。
    ///它可能不会返回所有程序集，但这些程序集与模块相关。
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// 获取所有程序集。
        /// </summary>
        /// <returns>程序集列表</returns>
        List<Assembly> GetAllAssemblies();
    }
}