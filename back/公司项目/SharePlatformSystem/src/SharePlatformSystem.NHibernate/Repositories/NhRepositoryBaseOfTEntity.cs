using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Domain.Repositories;

namespace SharePlatformSystem.NHibernate.Repositories
{
    /// <summary>
    /// A shortcut of <see cref="NhRepositoryBase{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class NhRepositoryBase<TEntity> : NhRepositoryBase<TEntity, string>, IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        /// <summary>
        /// Creates a new <see cref="NhRepositoryBase{TEntity,TPrimaryKey}"/> object.
        /// </summary>
        /// <param name="sessionProvider">A session provider to obtain session for database operations</param>
        public NhRepositoryBase(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }
    }
}