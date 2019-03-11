using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// �����¼�������ʵ�����֮ǰ֪ͨ��
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityUpdatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="entity">���ڸ��µ�ʵ��</param>
        public EntityUpdatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}