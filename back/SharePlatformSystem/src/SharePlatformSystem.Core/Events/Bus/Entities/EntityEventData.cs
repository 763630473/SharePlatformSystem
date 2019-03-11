using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 用于传递与<see cref=“ientity”/>对象相关的事件的数据。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityEventData<TEntity> : EventData , IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// 与此事件相关的实体。
        /// </summary>
        public TEntity Entity { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">与此事件相关的实体</param>
        public EntityEventData(TEntity entity)
        {
            Entity = entity;
        }

        public virtual object[] GetConstructorArgs()
        {
            return new object[] { Entity };
        }
    }
}