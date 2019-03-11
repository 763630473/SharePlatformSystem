using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件用于在实体更新之前通知。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityUpdatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">正在更新的实体</param>
        public EntityUpdatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}