using System.Collections.Generic;

namespace SharePlatformSystem.EntityHistory
{
    public interface IEntityHistorySelectorList : IList<NamedTypeSelector>
    {
        /// <summary>
        /// 按名称删除选择器。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool RemoveByName(string name);
    }
}
