using JetBrains.Annotations;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// 定义JSON格式的字符串属性以扩展对象/实体。
    /// </summary>
    public interface IExtendableObject
    {
        /// <summary>
        ///用于扩展包含对象的JSON格式字符串。
        ///JSON数据可以包含具有任意值的属性（如基元或复杂对象）。
        ///可以使用扩展方法“extendableObjectExtensions”）来操作此数据。
        ///常规格式：
        /// <code>
        /// {
        ///   "Property1" : ...
        ///   "Property2" : ...
        /// }
        /// </code>
        /// </summary>
        [CanBeNull]
        string ExtensionData { get; set; }
    }
}
