using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// �����¼������ڴ���ʵ��֮ǰ֪ͨ��
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="entity">������ʵ��</param>
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}