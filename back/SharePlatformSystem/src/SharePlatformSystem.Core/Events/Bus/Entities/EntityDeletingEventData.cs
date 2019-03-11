using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件用于在删除实体之前通知。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityDeletingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">正在删除的实体</param>
        public EntityDeletingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}