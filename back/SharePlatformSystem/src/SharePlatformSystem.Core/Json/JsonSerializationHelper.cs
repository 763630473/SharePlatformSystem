using System;
using Newtonsoft.Json;

namespace SharePlatformSystem.Json
{
    /// <summary>
    ///定义用于JSON的助手方法。
    /// </summary>
    public static class JsonSerializationHelper
    {
        private const char TypeSeperator = '|';

        /// <summary>
        ///序列化包含类型信息的对象。
        ///因此，可以稍后使用<see cref="DeserializeWithType"/>方法对其进行反序列化。
        /// </summary>
        public static string SerializeWithType(object obj)
        {
            return SerializeWithType(obj, obj.GetType());
        }

        /// <summary>
        ///序列化包含类型信息的对象。

        ///因此，可以稍后使用<see cref="DeserializeWithType"/>方法对其进行反序列化。
        /// </summary>
        public static string SerializeWithType(object obj, Type type)
        {
            var serialized = obj.ToJsonString();

            return string.Format(
                "{0}{1}{2}",
                type.AssemblyQualifiedName,
                TypeSeperator,
                serialized
                );
        }

        /// <summary>
        ///反序列化使用<see cref="serializewithtype（object）"/>方法序列化的对象。
        /// </summary>
        public static T DeserializeWithType<T>(string serializedObj)
        {
            return (T)DeserializeWithType(serializedObj);
        }

        /// <summary>
        ///反序列化使用<see cref="serializewithtype（object）"/>方法序列化的对象。
        /// </summary>
        public static object DeserializeWithType(string serializedObj)
        {
            var typeSeperatorIndex = serializedObj.IndexOf(TypeSeperator);
            var type = Type.GetType(serializedObj.Substring(0, typeSeperatorIndex));
            var serialized = serializedObj.Substring(typeSeperatorIndex + 1);

            var options = new JsonSerializerSettings
            {
                ContractResolver = new SharePlatformCamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.DeserializeObject(serialized, type, options);
        }
    }
}