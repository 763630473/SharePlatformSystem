using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 此类事件用于在创建实体之前通知。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">创建的实体</param>
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}