using System;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 定义使用字典进行配置的接口。
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// 用于设置名为configuration的字符串。
        ///如果已经存在具有相同“name”的配置，则它将被覆盖。
        /// </summary>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="value">配置的值</param>
        /// <returns>返回传递的值</returns>
        void Set<T>(string name, T value);

        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <param name="name">配置的唯一名称</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        object Get(string name);

        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="name">配置的唯一名称</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        T Get<T>(string name);

        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="defaultValue">如果找不到给定配置，则为对象的默认值</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        object Get(string name, object defaultValue);

        /// <summary>
        ///获取具有给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="defaultValue">如果找不到给定配置，则为对象的默认值</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        T Get<T>(string name, T defaultValue);
        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="creator">如果找不到给定的配置，将调用以创建的函数</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        T GetOrCreate<T>(string name, Func<T> creator);
    }
}