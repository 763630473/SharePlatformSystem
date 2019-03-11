using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Xml.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace SharePlatformSystem.core.Localization.Dictionaries.Xml
{
    /// <summary>
    /// 此类用于从XML构建本地化字典。
    /// </summary>
    /// <remarks>
    /// 使用静态生成方法创建此类的实例。
    /// </remarks>
    public class XmlLocalizationDictionary : LocalizationDictionary
    {
        /// <summary>
        /// 私有构造函数。
        /// </summary>
        /// <param name="cultureInfo">字典文化</param>
        private XmlLocalizationDictionary(CultureInfo cultureInfo)
            : base(cultureInfo)
        {

        }

        /// <summary>
        /// 从给定文件生成“xmlLocalizionDictionary”。
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        public static XmlLocalizationDictionary BuildFomFile(string filePath)
        {
            try
            {
                return BuildFomXmlString(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                throw new SharePlatformException("本地化文件格式无效！ " + filePath, ex);
            }
        }

        /// <summary>
        /// 从给定的XML字符串生成“xmlLocalizionDictionary”。
        /// </summary>
        /// <param name="xmlString">XML字符串</param>
        public static XmlLocalizationDictionary BuildFomXmlString(string xmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);

            var localizationDictionaryNode = xmlDocument.SelectNodes("/localizationDictionary");
            if (localizationDictionaryNode == null || localizationDictionaryNode.Count <= 0)
            {
                throw new SharePlatformException("本地化XML必须包含本地化字典作为根节点。");
            }

            var cultureName = localizationDictionaryNode[0].GetAttributeValueOrNull("culture");
            if (string.IsNullOrEmpty(cultureName))
            {
                throw new SharePlatformException("语言XML文件中未定义区域性！");
            }

            var dictionary = new XmlLocalizationDictionary(CultureInfo.GetCultureInfo(cultureName));

            var dublicateNames = new List<string>();

            var textNodes = xmlDocument.SelectNodes("/localizationDictionary/texts/text");
            if (textNodes != null)
            {
                foreach (XmlNode node in textNodes)
                {
                    var name = node.GetAttributeValueOrNull("name");
                    if (string.IsNullOrEmpty(name))
                    {
                        throw new SharePlatformException("在给定的XML字符串中，文本的name属性为空。");
                    }

                    if (dictionary.Contains(name))
                    {
                        dublicateNames.Add(name);
                    }

                    dictionary[name] = (node.GetAttributeValueOrNull("value") ?? node.InnerText).NormalizeLineEndings();
                }
            }

            if (dublicateNames.Count > 0)
            {
                throw new SharePlatformException("字典不能包含同一个键两次。有一些重复的名字: " + dublicateNames.JoinAsString(", "));
            }

            return dictionary;
        }
    }
}
