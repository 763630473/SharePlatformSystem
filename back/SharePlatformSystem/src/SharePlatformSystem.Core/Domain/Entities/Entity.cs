using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// 最常用的主键类型“string”的“entity”快捷方式。
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<string>, IEntity<string>
    {

    }
    /// <summary>
    /// IENTINTY接口的基本实现。
    //这是一个非常重要的问题。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 此实体的唯一标识符。
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// 检查此实体是否是临时的（它没有ID）。
        /// </summary>
        /// <returns>真的，如果这整件事是瞬间的</returns>
        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            {
                return true;
            }

            //ef core的解决方法，因为它在附加到dbcontext时将int/long设置为min值
            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TPrimaryKey>))
            {
                return false;
            }

            //相同的实例必须视为相等
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //瞬变物体不被认为是相等的
            var other = (Entity<TPrimaryKey>)obj;
            if (IsTransient() && other.IsTransient())
            {
                return false;
            }

            //必须具有类型的is-a关系或必须是相同的类型
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            if (Id == null)
            {
                return 0;
            }

            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
