using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// �����¼���������ɾ��ʵ�������֪ͨ��
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="entity">ɾ����ʵ��</param>
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}