using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Reflection;
using System;
using System.Reflection;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// ʵ���һЩ����������
    /// </summary>
    public static class EntityHelper
    {
        public static bool IsEntity(Type type)
        {
            return ReflectionHelper.IsAssignableToGenericType(type, typeof(IEntity<>));
        }

        public static Type GetPrimaryKeyType<TEntity>()
        {
            return GetPrimaryKeyType(typeof(TEntity));
        }

        /// <summary>
        /// ��ȡ����ʵ�����͵���������
        /// </summary>
        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            throw new SharePlatformException("�Ҳ�������ʵ�����͵���������: " + entityType + ".ȷ����ʵ������ʵ��IEntity<TPrimaryKey>Interface");
        }

        public static object GetEntityId(object entity)
        {
            if (!ReflectionHelper.IsAssignableToGenericType(entity.GetType(), typeof(IEntity<>)))
            {
                throw new SharePlatformException(entity.GetType() + "����ʵ��!");
            }

            return ReflectionHelper.GetValueByPath(entity, entity.GetType(), "Id");
        }

        public static string GetHardDeleteKey(object entity)
        {
            return entity.GetType().FullName + ";Id=" + GetEntityId(entity);
        }
    }
}