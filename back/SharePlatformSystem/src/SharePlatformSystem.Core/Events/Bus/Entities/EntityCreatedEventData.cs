using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// �����¼��������ڴ���ʵ�������֪ͨ��
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="entity">������ʵ��</param>
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}