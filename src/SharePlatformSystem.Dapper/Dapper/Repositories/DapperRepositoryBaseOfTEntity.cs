
using SharePlatformSystem.Core.Data;
using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity> : DapperRepositoryBase<TEntity, string>, IDapperRepository<TEntity>
        where TEntity : class, IEntity<string>
    {
        public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider) : base(activeTransactionProvider)
        {
        }
    }
}
