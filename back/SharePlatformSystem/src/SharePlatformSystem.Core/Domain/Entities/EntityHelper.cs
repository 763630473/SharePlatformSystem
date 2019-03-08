using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Reflection;
using System;
using System.Reflection;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// 实体的一些辅助方法。
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
        /// 获取给定实体类型的主键类型
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

            throw new SharePlatformException("找不到给定实体类型的主键类型: " + entityType + ".确保此实体类型实现IEntity<TPrimaryKey>Interface");
        }

        public static object GetEntityId(object entity)
        {
            if (!ReflectionHelper.IsAssignableToGenericType(entity.GetType(), typeof(IEntity<>)))
            {
                throw new SharePlatformException(entity.GetType() + "不是实体!");
            }

            return ReflectionHelper.GetValueByPath(entity, entity.GetType(), "Id");
        }

        public static string GetHardDeleteKey(object entity)
        {
            return entity.GetType().FullName + ";Id=" + GetEntityId(entity);
        }
    }
}