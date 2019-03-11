using System.Collections.Generic;

namespace SharePlatformSystem.core.Localization.Dictionaries.Json
{
    /// <summary>
    /// 使用它来序列化JSON文件
    /// </summary>
    public class JsonLocalizationFile
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public JsonLocalizationFile()
        {
            Texts = new Dictionary<string, string>();
        }

        /// <summary>
        ///获取或设置文化名称；例如：en、en-us、zh-cn
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// 键值对
        /// </summary>
        public Dictionary<string, string> Texts { get; private set; }
    }
}