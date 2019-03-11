using SharePlatformSystem.Core.Exceptions;
using System.Linq;
using System.Xml;

namespace SharePlatformSystem.Core.Xml.Extensions
{
    /// <summary>
    /// 类的扩展方法。 <see cref="XmlNode"/> .
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// 从XML节点获取属性的值。
        /// </summary>
        /// <param name="node">XML节点</param>
        /// <param name="attributeName">属性名</param>
        /// <returns>属性的值</returns>
        public static string GetAttributeValueOrNull(this XmlNode node, string attributeName)
        {
            if (node.Attributes == null || node.Attributes.Count <= 0)
            {
                throw new SharePlatformException(node.Name + " 节点没有 " + attributeName + " 属性");
            }

            return node.Attributes
                .Cast<XmlAttribute>()
                .Where(attr => attr.Name == attributeName)
                .Select(attr => attr.Value)
                .FirstOrDefault();
        }
    }
}
