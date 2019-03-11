using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Collections
{
    /// <summary>
    ///"ITypeList{TBaseType}"使用对象作为基类型的快捷方式。
    /// </summary>
    public interface ITypeList : ITypeList<object>
    {

    }

    /// <summary>
    /// 扩展"IList{Type}"以添加对特定基类型的限制。
    /// </summary>
    /// <typeparam name="TBaseType">此列表中“Type”的基类型</typeparam>
    public interface ITypeList<in TBaseType> : IList<Type>
    {
        /// <summary>
        /// 向列表中添加类型。
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// 检查列表中是否存在类型。
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// 从列表中删除类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Remove<T>() where T : TBaseType;
    }
}