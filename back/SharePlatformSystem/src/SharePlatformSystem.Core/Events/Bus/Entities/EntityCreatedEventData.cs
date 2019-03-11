using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件可用于在创建实体后立即通知。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="entity">创建的实体</param>
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}