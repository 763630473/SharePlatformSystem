using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Dapper.Repositories
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, string> where TEntity : class, IEntity<string>
    {
    }
}
