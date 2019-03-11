using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// �����¼�������ɾ��ʵ��֮ǰ֪ͨ��
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityDeletingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="entity">����ɾ����ʵ��</param>
        public EntityDeletingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}