using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Domain.Repositories;

namespace SharePlatformSystem.NHibernate.Repositories
{
    /// <summary>
    /// 最常用的主键类型（<see cref=“string”/>）的快捷方式。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class NhRepositoryBase<TEntity> : NhRepositoryBase<TEntity, string>, IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        /// <summary>
        /// 创建一个新的对象。
        /// </summary>
        /// <param name="sessionProvider">用于获取数据库操作会话的会话提供程序</param>
        public NhRepositoryBase(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }
    }
}