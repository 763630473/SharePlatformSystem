using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharePlatformSystem.Runtime.Serialization
{
    /// <summary>
    ///此类用于简化序列化/反序列化操作。
    ///使用.NET二进制序列化。    /// </summary>
    public static class BinarySerializationHelper
    {
        /// <summary>
        ///序列化对象并作为字节数组返回。
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>对象的字节数</returns>
        public static byte[] Serialize(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                CreateBinaryFormatter().Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 将对象序列化为流。
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="stream">要序列化的流</param>
        /// <returns>对象字节数</returns>
        public static void Serialize(object obj, Stream stream)
        {
            CreateBinaryFormatter().Serialize(stream, obj);
        }

        /// <summary>
        /// 从给定的字节数组反序列化对象。
        /// </summary>
        /// <param name="bytes">包含对象的字节数组</param>
        /// <returns>反序列化对象</returns>
        public static object Deserialize(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return CreateBinaryFormatter().Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// 从给定流反序列化对象。
        /// </summary>
        /// <param name="stream">包含对象的流</param>
        /// <returns>反序列化对象</returns> 
        public static object Deserialize(Stream stream)
        {
            return CreateBinaryFormatter().Deserialize(stream);
        }

        /// <summary>
        ///反序列化给定字节数组中的对象。
        ///与<see cref=“deserialize（byte[]）”/>的区别在于；此方法还可以反序列化
        ///在动态加载的程序集中定义的类型（如插件）。
        /// </summary>
        /// <param name="bytes">包含对象的字节数组</param>
        /// <returns>反序列化对象</returns>        
        public static object DeserializeExtended(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return CreateBinaryFormatter(true).Deserialize(memoryStream);
            }
        }

        /// <summary>
        ///反序列化给定流中的对象。
        ///与<see cref=“deserialize（stream）”/>的区别在于，此方法还可以反序列化
        ///在动态加载的程序集中定义的类型（如插件）。
        /// </summary>
        /// <param name="stream">包含对象的流</param>
        /// <returns>反序列化对象</returns> 
        public static object DeserializeExtended(Stream stream)
        {
            return CreateBinaryFormatter(true).Deserialize(stream);
        }

        private static BinaryFormatter CreateBinaryFormatter(bool extended = false)
        {
            if (extended)
            {
                return new BinaryFormatter
                {
                    //TODO: AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
                    Binder = new ExtendedSerializationBinder()
                };
            }
            else
            {
                return new BinaryFormatter();
            }
        }

        /// <summary>
        ///此类用于反序列化，以允许反序列化定义的对象
        ///在运行时加载的assemlies中（如插件）。
        /// </summary>
        private sealed class ExtendedSerializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                var toAssemblyName = assemblyName.Split(',')[0];
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.FullName.Split(',')[0] == toAssemblyName)
                    {
                        return assembly.GetType(typeName);
                    }
                }

                return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
            }
        }
    }
}
