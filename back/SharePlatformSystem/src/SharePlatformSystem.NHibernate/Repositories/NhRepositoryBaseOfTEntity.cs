using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Domain.Repositories;

namespace SharePlatformSystem.NHibernate.Repositories
{
    /// <summary>
    /// ��õ��������ͣ�<see cref=��string��/>���Ŀ�ݷ�ʽ��
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    public class NhRepositoryBase<TEntity> : NhRepositoryBase<TEntity, string>, IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        /// <summary>
        /// ����һ���µĶ���
        /// </summary>
        /// <param name="sessionProvider">���ڻ�ȡ���ݿ�����Ự�ĻỰ�ṩ����</param>
        public NhRepositoryBase(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }
    }
}