﻿using SharePlatformSystem.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharePlatformSystem.Runtime.Caching
{
    /// <summary>
    /// 一个类，用于保存缓存中存储的数据的类型信息和序列化负载。
    /// </summary>
    public class SharePlatformCacheData
    {
        private static readonly IReadOnlyList<string> SystemAssemblyNames = new List<string> { "mscorlib", "System.Private.CoreLib" };

        public SharePlatformCacheData(
            string type, string payload)
        {
            Type = type;
            Payload = payload;
        }

        public string Payload { get; set; }

        public string Type { get; set; }

        public static SharePlatformCacheData Deserialize(string serializedCacheData) => serializedCacheData.FromJsonString<SharePlatformCacheData>();

        public static SharePlatformCacheData Serialize(object obj)
        {
            return new SharePlatformCacheData(
                SerializeType(obj.GetType()).ToString(),
                obj.ToJsonString());
        }

        private static StringBuilder SerializeType(Type type, bool withAssemblyName = true, StringBuilder typeNameBuilder = null)
        {
            typeNameBuilder = typeNameBuilder ?? new StringBuilder();

            if (type.DeclaringType != null)
            {
                SerializeType(type.DeclaringType, false, typeNameBuilder).Append('+');
            }
            else if (type.Namespace != null)
            {
                typeNameBuilder.Append(type.Namespace).Append('.');
            }

            typeNameBuilder.Append(type.Name);

            if (type.GenericTypeArguments.Length > 0)
            {
                SerializeTypes(type.GenericTypeArguments, '[', ']', typeNameBuilder);
            }

            if (!withAssemblyName)
            {
                return typeNameBuilder;
            }

            var assemblyName = type.GetTypeInfo().Assembly.GetName().Name;

            if (!SystemAssemblyNames.Contains(assemblyName))
            {
                typeNameBuilder.Append(", ").Append(assemblyName);
            }

            return typeNameBuilder;
        }

        private static StringBuilder SerializeTypes(Type[] types, char beginTypeDelimiter = '"', char endTypeDelimiter = '"', StringBuilder typeNamesBuilder = null)
        {
            if (types == null)
            {
                return null;
            }

            if (typeNamesBuilder == null)
            {
                typeNamesBuilder = new StringBuilder();
            }

            typeNamesBuilder.Append('[');

            for (int i = 0; i < types.Length; i++)
            {
                typeNamesBuilder.Append(beginTypeDelimiter);
                SerializeType(types[i], true, typeNamesBuilder);
                typeNamesBuilder.Append(endTypeDelimiter);

                if (i != types.Length - 1)
                {
                    typeNamesBuilder.Append(',');
                }
            }

            return typeNamesBuilder.Append(']');
        }
    }
}