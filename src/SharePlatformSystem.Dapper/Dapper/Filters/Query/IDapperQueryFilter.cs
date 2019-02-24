using System;
using System.Linq.Expressions;
using DapperExtensions;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Dapper.Filters.Query
{
    public interface IDapperQueryFilter : ITransientDependency
    {
        string FilterName { get; }

        bool IsEnabled { get; }

        IFieldPredicate ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>;

        Expression<Func<TEntity, bool>> ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>;
    }
}
