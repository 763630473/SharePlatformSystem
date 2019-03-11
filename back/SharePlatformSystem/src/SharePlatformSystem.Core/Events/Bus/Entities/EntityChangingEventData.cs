using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    ///用于在更改实体（<see cref=“ientity”/>）时传递事件的数据（创建、更新或删除）。
    ///请参见<see cref=“entityCreatingEventData tentity”/>，<see cref=“entityDeletingEventData tentity”/>和<see cref=“entityUpdingEventData tentity”/>类。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    [Serializable]
    public class EntityChangingEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="entity">在此事件中更改实体</param>
        public EntityChangingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}