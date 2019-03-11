using SharePlatformSystem.Core.Reflection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace SharePlatformSystem.Dapper.Utils
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// 检查是否<paramref name="child" />实现/继承<paramref name=“parent”/>
        /// </summary>
        /// <param name="child">当前类型</param>
        /// <param name="parent">父类型</param>
        /// <returns></returns>
        public static bool IsInheritsOrImplements(this Type child, Type parent)
        {
            if (child == parent)
            {
                return false;
            }

            if (child.GetTypeInfo().IsSubclassOf(parent))
            {
                return true;
            }

            Type[] parameters = parent.GetGenericArguments();

            bool isParameterLessGeneric = !(parameters.Length > 0 &&
                                            (parameters[0].GetTypeInfo().Attributes & TypeAttributes.BeforeFieldInit) ==
                                            TypeAttributes.BeforeFieldInit);

            while (child != null && child != typeof(object))
            {
                Type cur = GetFullTypeDefinition(child);
                if (parent == cur ||
                    isParameterLessGeneric &&
                    cur.GetInterfaces().Select(GetFullTypeDefinition).Contains(GetFullTypeDefinition(parent)))
                {
                    return true;
                }

                if (!isParameterLessGeneric)
                {
                    if (GetFullTypeDefinition(parent) == cur && !cur.GetTypeInfo().IsInterface)
                    {
                        if (VerifyGenericArguments(GetFullTypeDefinition(parent), cur))
                        {
                            if (VerifyGenericArguments(parent, child))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (child.GetInterfaces()
                            .Where(i => GetFullTypeDefinition(parent) == GetFullTypeDefinition(i))
                            .Any(item => VerifyGenericArguments(parent, item)))
                        {
                            return true;
                        }
                    }
                }

                child = child.GetTypeInfo().BaseType;
            }

            return false;
        }

        private static Type GetFullTypeDefinition(Type type)
        {
            return type.GetTypeInfo().IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        private static bool VerifyGenericArguments(Type parent, Type child)
        {
            Type[] childArguments = child.GetGenericArguments();
            Type[] parentArguments = parent.GetGenericArguments();
            if (childArguments.Length == parentArguments.Length)
            {
                for (var i = 0; i < childArguments.Length; i++)
                {
                    if (childArguments[i].GetAssembly() != parentArguments[i].GetAssembly() ||
                        childArguments[i].Name != parentArguments[i].Name ||
                        childArguments[i].Namespace != parentArguments[i].Namespace)
                    {
                        if (!childArguments[i].GetTypeInfo().IsSubclassOf(parentArguments[i]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
