using JetBrains.Annotations;
using Newtonsoft.Json;
using System;

namespace SharePlatformSystem.Json
{
    public static class JsonExtensions
    {
        /// <summary>
        ///将给定对象转换为JSON字符串。
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var settings = new JsonSerializerSettings();

            if (camelCase)
            {
                settings.ContractResolver = new SharePlatformCamelCasePropertyNamesContractResolver();
            }
            else
            {
                settings.ContractResolver = new SharePlatformContractResolver();
            }

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }
            
            return ToJsonString(obj, settings);
        }

        /// <summary>
        /// 使用自定义将给定对象转换为JSON字符串 <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, JsonSerializerSettings settings)
        {
            return obj != null
                ? JsonConvert.SerializeObject(obj, settings)
                : default(string);
        }

        /// <summary>
        /// 使用默认值返回反序列化字符串 <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value)
        {
            return value.FromJsonString<T>(new JsonSerializerSettings());
        }

        /// <summary>
        /// 使用自定义返回反序列化字符串 <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value, JsonSerializerSettings settings)
        {
            return value != null
                ? JsonConvert.DeserializeObject<T>(value, settings)
                : default(T);
        }

        /// <summary>
        /// 使用显式返回反序列化字符串 <see cref="Type"/> and custom <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static object FromJsonString(this string value, [NotNull] Type type, JsonSerializerSettings settings)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return value != null
                ? JsonConvert.DeserializeObject(value, type, settings)
                : null;
        }
    }
}