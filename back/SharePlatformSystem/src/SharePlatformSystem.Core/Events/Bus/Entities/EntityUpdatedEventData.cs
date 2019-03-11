using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// �����¼���������ʵ����º�����֪ͨ��
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="entity">���µ�ʵ��</param>
        public EntityUpdatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}