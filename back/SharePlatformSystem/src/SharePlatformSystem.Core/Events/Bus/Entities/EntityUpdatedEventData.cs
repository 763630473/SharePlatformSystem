using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件可用于在实体更新后立即通知。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">更新的实体</param>
        public EntityUpdatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}