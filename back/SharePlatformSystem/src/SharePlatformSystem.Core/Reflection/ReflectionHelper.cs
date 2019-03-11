using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharePlatformSystem.Core.Reflection
{
    /// <summary>
    /// 定义反射的辅助方法。
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        ///检查是否实现/继承
        /// </summary>
        /// <param name="givenType">Type to check</param>
        /// <param name="genericType">Generic type</param>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var givenTypeInfo = givenType.GetTypeInfo();

            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
        }

        /// <summary>
        ///获取为类成员定义的属性列表，该属性声明的类型包括继承的属性。
        /// </summary>
        /// <param name="inherit">从基类继承属性</param>
        /// <param name="memberInfo">MemberInfo</param>
        public static List<object> GetAttributesOfMemberAndDeclaringType(MemberInfo memberInfo, bool inherit = true)
        {
            var attributeList = new List<object>();

            attributeList.AddRange(memberInfo.GetCustomAttributes(inherit));

            if (memberInfo.DeclaringType != null)
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(inherit));
            }

            return attributeList;
        }

        /// <summary>
        ///获取为类成员和类型（包括继承的属性）定义的属性列表。
        /// </summary>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="type">Type</param>
        /// <param name="inherit">从基类继承属性</param>
        public static List<object> GetAttributesOfMemberAndType(MemberInfo memberInfo, Type type, bool inherit = true)
        {
            var attributeList = new List<object>();
            attributeList.AddRange(memberInfo.GetCustomAttributes(inherit));
            attributeList.AddRange(type.GetTypeInfo().GetCustomAttributes(inherit));
            return attributeList;
        }

        /// <summary>
        ///获取为类成员定义的属性列表，该属性声明的类型包括继承的属性。
        /// </summary>
        /// <typeparam name="TAttribute">属性的类型</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="inherit">从基类继承属性</param>
        public static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            var attributeList = new List<TAttribute>();

            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>());
            }

            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.GetTypeInfo().IsDefined(typeof(TAttribute), inherit))
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>());
            }

            return attributeList;
        }

        /// <summary>
        /// 获取为类成员和类型（包括继承的属性）定义的属性列表。
        /// </summary>
        /// <typeparam name="TAttribute">属性的类型</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="type">Type</param>
        /// <param name="inherit">从基类继承属性</param>
        public static List<TAttribute> GetAttributesOfMemberAndType<TAttribute>(MemberInfo memberInfo, Type type, bool inherit = true)
            where TAttribute : Attribute
        {
            var attributeList = new List<TAttribute>();

            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>());
            }

            if (type.GetTypeInfo().IsDefined(typeof(TAttribute), inherit))
            {
                attributeList.AddRange(type.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>());
            }

            return attributeList;
        }

        /// <summary>
        /// 尝试获取为类成员定义的of属性，它声明的类型包括继承的属性。
        ///如果没有声明，则返回默认值。
        /// </summary>
        /// <typeparam name="TAttribute">属性的类型</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="defaultValue">默认值（默认为空）</param>
        /// <param name="inherit">从基类继承属性</param>
        public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
            where TAttribute : class
        {
            return memberInfo.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
                   ?? memberInfo.ReflectedType?.GetTypeInfo().GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
                   ?? defaultValue;
        }

        /// <summary>
        /// 尝试获取为类成员定义的of属性，它声明的类型包括继承的属性。
        ///如果没有声明，则返回默认值。
        /// </summary>
        /// <typeparam name="TAttribute">属性的类型</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="defaultValue">默认值（默认为空）</param>
        /// <param name="inherit">从基类继承属性</param>
        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
            where TAttribute : Attribute
        {
            //Get attribute on the member
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }

        /// <summary>
        ///根据给定对象的完整路径获取属性
        /// </summary>
        /// <param name="obj">从中获取值的对象</param>
        /// <param name="objectType">给定对象的类型</param>
        /// <param name="propertyPath">财产的完整路径</param>
        /// <returns></returns>
        public static object GetPropertyByPath(object obj, Type objectType, string propertyPath)
        {
            var property = obj;
            var currentType = objectType;
            var objectPath = currentType.FullName;
            var absolutePropertyPath = propertyPath;
            if (absolutePropertyPath.StartsWith(objectPath))
            {
                absolutePropertyPath = absolutePropertyPath.Replace(objectPath + ".", "");
            }

            foreach (var propertyName in absolutePropertyPath.Split('.'))
            {
                property = currentType.GetProperty(propertyName);
                currentType = ((PropertyInfo) property).PropertyType;
            }

            return property;
        }

        /// <summary>
        ///根据给定对象的完整路径获取属性值
        /// </summary>
        /// <param name="obj">从中获取值的对象</param>
        /// <param name="objectType">给定对象的类型</param>
        /// <param name="propertyPath">财产的完整路径/param>
        /// <returns></returns>
        public static object GetValueByPath(object obj, Type objectType, string propertyPath)
        {
            var value = obj;
            var currentType = objectType;
            var objectPath = currentType.FullName;
            var absolutePropertyPath = propertyPath;
            if (absolutePropertyPath.StartsWith(objectPath))
            {
                absolutePropertyPath = absolutePropertyPath.Replace(objectPath + ".", "");
            }

            foreach (var propertyName in absolutePropertyPath.Split('.'))
            {
                var property = currentType.GetProperty(propertyName);
                value = property.GetValue(value, null);
                currentType = property.PropertyType;
            }

            return value;
        }

        /// <summary>
        /// 按给定对象的完整路径设置属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objectType"></param>
        /// <param name="propertyPath"></param>
        /// <param name="value"></param>
        internal static void SetValueByPath(object obj, Type objectType, string propertyPath, object value)
        {
            var currentType = objectType;
            PropertyInfo property;
            var objectPath = currentType.FullName;
            var absolutePropertyPath = propertyPath;
            if (absolutePropertyPath.StartsWith(objectPath))
            {
                absolutePropertyPath = absolutePropertyPath.Replace(objectPath + ".", "");
            }

            var properties = absolutePropertyPath.Split('.');

            if (properties.Length == 1)
            {
                property = objectType.GetProperty(properties.First());
                property.SetValue(obj, value);
                return;
            }

            for (int i = 0; i < properties.Length - 1; i++)
            {
                property = currentType.GetProperty(properties[i]);
                obj = property.GetValue(obj, null);
                currentType = property.PropertyType;
            }

            property = currentType.GetProperty(properties.Last());
            property.SetValue(obj, value);
        }

        internal static bool IsPropertyGetterSetterMethod(MethodInfo method, Type type)
        {
            if (!method.IsSpecialName)
            {
                return false;
            }

            if (method.Name.Length < 5)
            {
                return false;
            }

            return type.GetProperty(method.Name.Substring(4), BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic) != null;
        }
    }
}