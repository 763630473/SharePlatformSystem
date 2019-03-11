using System.Collections.Generic;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// 选择要审核的类/接口的选择器函数列表。
    /// </summary>
    public interface IAuditingSelectorList : IList<NamedTypeSelector>
    {
        /// <summary>
        /// 按名称删除选择器。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool RemoveByName(string name);
    }
}