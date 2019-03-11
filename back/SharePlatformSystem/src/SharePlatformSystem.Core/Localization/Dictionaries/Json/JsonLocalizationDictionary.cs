using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.core.Localization.Dictionaries.Json
{
    /// <summary>
    /// 此类用于从JSON构建本地化字典。
    /// </summary>
    /// <remarks>
    /// 使用静态生成方法创建此类的实例。
    /// </remarks>
    public class JsonLocalizationDictionary : LocalizationDictionary
    {
        /// <summary>
        /// 私有构造函数。
        /// </summary>
        /// <param name="cultureInfo">字典文化</param>
        private JsonLocalizationDictionary(CultureInfo cultureInfo)
            : base(cultureInfo)
        {
        }

        /// <summary>
        ///从给定文件生成“jsonLocalizationDictionary”。
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static JsonLocalizationDictionary BuildFromFile(string filePath)
        {
            try
            {
                return BuildFromJsonString(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                throw new SharePlatformException("本地化文件格式无效！" + filePath, ex);
            }
        }

        /// <summary>
        ///     Builds an <see cref="JsonLocalizationDictionary" /> from given json string.
        /// </summary>
        /// <param name="jsonString">Json string</param>
        public static JsonLocalizationDictionary BuildFromJsonString(string jsonString)
        {
            JsonLocalizationFile jsonFile;
            try
            {
                jsonFile = JsonConvert.DeserializeObject<JsonLocalizationFile>(
                    jsonString,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            catch (JsonException ex)
            {
                throw new SharePlatformException("无法分析JSON字符串。 " + ex.Message);
            }

            var cultureCode = jsonFile.Culture;
            if (string.IsNullOrEmpty(cultureCode))
            {
                throw new SharePlatformException("语言JSON文件中的区域性为空。");
            }

            var dictionary = new JsonLocalizationDictionary(CultureInfo.GetCultureInfo(cultureCode));
            var dublicateNames = new List<string>();
            foreach (var item in jsonFile.Texts)
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    throw new SharePlatformException("给定JSON字符串中的键为空。");
                }

                if (dictionary.Contains(item.Key))
                {
                    dublicateNames.Add(item.Key);
                }

                dictionary[item.Key] = item.Value.NormalizeLineEndings();
            }

            if (dublicateNames.Count > 0)
            {
                throw new SharePlatformException(
                    "字典不能包含同一个键两次。有一些重复的名称： " +
                    dublicateNames.JoinAsString(", "));
            }

            return dictionary;
        }
    }
}