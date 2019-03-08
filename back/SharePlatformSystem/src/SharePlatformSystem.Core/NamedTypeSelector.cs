using System;

namespace SharePlatformSystem
{
    /// <summary>
    /// 用于表示命名类型选择器。
    /// </summary>
    public class NamedTypeSelector
    {
        /// <summary>
        /// 选择器的名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 谓语。
        /// </summary>
        public Func<Type, bool> Predicate { get; set; }

        /// <summary>
        /// 创建新的“NamedTypeSelector”对象。
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="predicate">谓语</param>
        public NamedTypeSelector(string name, Func<Type, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}