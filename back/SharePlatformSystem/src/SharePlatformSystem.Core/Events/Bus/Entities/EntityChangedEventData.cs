using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    /// ������ʵ�壨<see cref=��ientity��/>�����ģ����������»�ɾ����ʱ�����¼������ݡ�
    ///��μ�<see cref=��entityCreatedEventData tentity��/>��<see cref=��entityDeletedEventData tentity��/>��<see cref=��entityUpdatedEventData tentity��/>�ࡣ
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="entity">���¼��е��Ѹ���ʵ��</param>
        public EntityChangedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}