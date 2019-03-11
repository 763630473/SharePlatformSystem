using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件可用于在删除实体后立即通知。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">删除的实体</param>
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}