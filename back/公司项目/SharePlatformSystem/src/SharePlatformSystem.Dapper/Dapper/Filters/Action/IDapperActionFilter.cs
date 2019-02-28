using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Dapper.Filters.Action
{
    public interface IDapperActionFilter : ITransientDependency
    {
        void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>;
    }
}
