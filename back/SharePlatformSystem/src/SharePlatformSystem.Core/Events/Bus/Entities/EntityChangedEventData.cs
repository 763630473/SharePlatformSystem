using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// 用于在实体（<see cref=“ientity”/>）更改（创建、更新或删除）时传递事件的数据。
    ///请参见<see cref=“entityCreatedEventData tentity”/>，<see cref=“entityDeletedEventData tentity”/>和<see cref=“entityUpdatedEventData tentity”/>类。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="entity">此事件中的已更改实体</param>
        public EntityChangedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}