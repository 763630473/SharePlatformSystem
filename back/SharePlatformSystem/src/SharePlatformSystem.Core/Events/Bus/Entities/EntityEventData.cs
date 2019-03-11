using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// ���ڴ�����<see cref=��ientity��/>������ص��¼������ݡ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityEventData<TEntity> : EventData , IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// ����¼���ص�ʵ�塣
        /// </summary>
        public TEntity Entity { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="entity">����¼���ص�ʵ��</param>
        public EntityEventData(TEntity entity)
        {
            Entity = entity;
        }

        public virtual object[] GetConstructorArgs()
        {
            return new object[] { Entity };
        }
    }
}