using System;
using System.Linq.Expressions;
using DapperExtensions;
using JetBrains.Annotations;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Dapper.Expressions
{
    internal static class DapperExpressionExtensions
    {
        [NotNull]
        public static IPredicate ToPredicateGroup<TEntity, TPrimaryKey>([NotNull] this Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity<TPrimaryKey>
        {
            Check.NotNull(expression, nameof(expression));

            var dev = new DapperExpressionVisitor<TEntity, TPrimaryKey>();
            IPredicate pg = dev.Process(expression);

            return pg;
        }
    }
}
