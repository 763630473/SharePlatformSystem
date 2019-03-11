using System;

namespace SharePlatformSystem.Events.Bus.Entities
{
    /// <summary>
    ///�����ڸ���ʵ�壨<see cref=��ientity��/>��ʱ�����¼������ݣ����������»�ɾ������
    ///��μ�<see cref=��entityCreatingEventData tentity��/>��<see cref=��entityDeletingEventData tentity��/>��<see cref=��entityUpdingEventData tentity��/>�ࡣ
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    [Serializable]
    public class EntityChangingEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// ���캯��.
        /// </summary>
        /// <param name="entity">�ڴ��¼��и���ʵ��</param>
        public EntityChangingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}