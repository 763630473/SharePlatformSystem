using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SharePlatformSystem.Collections
{
    /// <summary>
    ///"TypeList{TBaseType}"的快捷方式，用于将对象用作基类型。
    /// </summary>
    public class TypeList : TypeList<object>, ITypeList
    {
    }

    /// <summary>
    /// 扩展"List{Type}"以添加对特定基类型的限制。
    /// </summary>
    /// <typeparam name="TBaseType">此列表中“Type”的基类型</typeparam>
    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        /// <summary>
        /// 获取计数。
        /// </summary>
        /// <value>计数器</value>
        public int Count { get { return _typeList.Count; } }

        /// <summary>
        /// 获取一个值，该值指示此实例是否为只读。
        /// </summary>
        /// <value>如果此实例是只读的，则为“真”；否则为“假”。</value>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// 获取或设置指定索引处的“Type”。
        /// </summary>
        /// <param name="index">索引.</param>
        public Type this[int index]
        {
            get { return _typeList[index]; }
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        private readonly List<Type> _typeList;

        /// <summary>
        /// 创建一个新的"TypeList{T}"对象。
        /// </summary>
        public TypeList()
        {
            _typeList = new List<Type>();
        }

        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }

        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        public void Insert(int index, Type item)
        {
            _typeList.Insert(index, item);
        }

        public int IndexOf(Type item)
        {
            return _typeList.IndexOf(item);
        }

        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }

        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        public void Remove<T>() where T : TBaseType
        {
            _typeList.Remove(typeof(T));
        }

        public bool Remove(Type item)
        {
            return _typeList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _typeList.RemoveAt(index);
        }

        public void Clear()
        {
            _typeList.Clear();
        }

        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
            {
                throw new ArgumentException("给定的项不是类型" + typeof(TBaseType).AssemblyQualifiedName, "项目");
            }
        }
    }
}