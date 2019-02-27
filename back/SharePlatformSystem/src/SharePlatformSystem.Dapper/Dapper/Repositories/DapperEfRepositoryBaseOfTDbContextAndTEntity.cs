
using SharePlatformSystem.Core.Data;
using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Dapper.Repositories
{
    public class DapperEfRepositoryBase<TDbContext, TEntity> : DapperEfRepositoryBase<TDbContext, TEntity, string>, IDapperRepository<TEntity>
        where TEntity : class, IEntity<string>
        where TDbContext : class

    {
        public DapperEfRepositoryBase(IActiveTransactionProvider activeTransactionProvider) : base(activeTransactionProvider)
        {
        }
    }
}
