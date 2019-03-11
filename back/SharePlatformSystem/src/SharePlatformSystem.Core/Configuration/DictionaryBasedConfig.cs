using SharePlatformSystem.Core.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 用于设置/获取自定义配置。
    /// </summary>
    public class DictionaryBasedConfig : IDictionaryBasedConfig
    {
        /// <summary>
        /// 自定义配置字典。
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }

        /// <summary>
        ///获取/设置配置值。
        ///如果没有给定名称的配置，则返回空值。
        /// </summary>
        /// <param name="name">配置的名称</param>
        /// <returns>配置的值</returns>
        public object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        ///构造器
        /// </summary>
        protected DictionaryBasedConfig()
        {
            CustomSettings = new Dictionary<string, object>();
        }

        /// <summary>
        /// 获取作为特定类型的配置值。
        /// </summary>
        /// <param name="name">配置的名称</param>
        /// <typeparam name="T">配置的类型</typeparam>
        /// <returns>配置的值，如果找不到，则为空</returns>
        public T Get<T>(string name)
        {
            var value = this[name];
            return value == null
                ? default(T)
                : (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        ///用于设置名为configuration的字符串。
        ///如果已经有一个具有相同“名称”的配置，则它将被覆盖。
        /// </summary>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="value">配置的值</param>
        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }

        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <param name="name">配置的唯一名称</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        public object Get(string name)
        {
            return Get(name, null);
        }

        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="defaultValue">如果找不到给定配置，则为对象的默认值</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        public object Get(string name, object defaultValue)
        {
            var value = this[name];
            if (value == null)
            {
                return defaultValue;
            }

            return this[name];
        }

        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="defaultValue">如果找不到给定配置，则为对象的默认值</param>
        /// <returns>配置的值，如果找不到，则为空</returns>
        public T Get<T>(string name, T defaultValue)
        {
            return (T)Get(name, (object)defaultValue);
        }

        /// <summary>
        /// 获取具有给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="name">配置的唯一名称</param>
        /// <param name="creator">如果找不到给定的配置，将调用以创建的函数</param>
        /// <returns>配置值，如果未找到则为空</returns>
        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);
            if (value == null)
            {
                value = creator();
                Set(name, value);
            }
            return (T)value;
        }
    }
}