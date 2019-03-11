using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Domain.Repositories;

namespace SharePlatformSystem.Dapper.Repositories
{
    /// <summary>
    ///dapper存储库抽象接口。
    /// </summary>
    /// <typeparam name="TEntity">实体的类型</typeparam>
    /// <typeparam name="TPrimaryKey">类型的主要关键。</typeparam>
    /// <seealso cref="IDapperRepository{TEntity,TPrimaryKey}" />
    public interface IDapperRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        ///获取指定的标识符。
        /// </summary>
        /// <param name="id">标识符。</param>
        /// <returns></returns>
        [NotNull]
        TEntity Single([NotNull] TPrimaryKey id);

        /// <summary>
        ///获取具有指定谓词的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [NotNull]
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取具有指定谓词的实体
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取异步。
        /// </summary>
        /// <param name="id">标识符。</param>
        /// <returns></returns>
        [NotNull]
        Task<TEntity> SingleAsync([NotNull] TPrimaryKey id);

        /// <summary>
        ///获取指定的标识符。
        /// </summary>
        /// <param name="id">标识符。</param>
        /// <returns></returns>
        [NotNull]
        TEntity Get([NotNull] TPrimaryKey id);

        /// <summary>
        ///获取异步。
        /// </summary>
        /// <param name="id">标识符。</param>
        /// <returns></returns>
        [NotNull]
        Task<TEntity> GetAsync([NotNull] TPrimaryKey id);

        /// <summary>
        ///获取指定的标识符。
        /// </summary>
        /// <param name="id">标识符。</param>
        /// <returns></returns>
        [CanBeNull]
        TEntity FirstOrDefault([NotNull] TPrimaryKey id);

        /// <summary>
        ///获取指定的标识符。
        /// </summary>
        /// <param name="id">标识符。</param>
        /// <returns></returns>
        [CanBeNull]
        Task<TEntity> FirstOrDefaultAsync([NotNull] TPrimaryKey id);

        /// <summary>
        ///获取具有指定谓词的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [CanBeNull]
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取具有指定谓词的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [CanBeNull]
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取列表。
        /// </summary>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAll();

        /// <summary>
        ///获取异步列表。
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        ///获取列表。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAll([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取异步列表。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取异步分页的列表。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        /// <param name="pageNumber">页码。</param>
        /// <param name="itemsPerPage">每页的项目数。</param>
        /// <param name="sortingProperty">排序属性。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllPagedAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, [NotNull] string sortingProperty, bool ascending = true);

        /// <summary>
        ///获取异步分页的列表。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        /// <param name="pageNumber">页码。</param>
        /// <param name="itemsPerPage">每页的项目数。</param>
        /// <param name="sortingExpression">排序表达式。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllPagedAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        /// <summary>
        ///获取已分页的列表。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        /// <param name="pageNumber">页码。</param>
        /// <param name="itemsPerPage">每页的项目数。</param>
        /// <param name="sortingExpression">排序表达式。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAllPaged([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, [NotNull] string sortingProperty, bool ascending = true);

        /// <summary>
        ///获取已分页的列表。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        /// <param name="pageNumber">页码。</param>
        /// <param name="itemsPerPage">每页的项目数。</param>
        /// <param name="sortingExpression">排序表达式。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetAllPaged([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        /// <summary>
        ///对指定的谓词进行计数。
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <returns></returns>
        int Count([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///计算异步。
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <returns></returns>
        [NotNull]
        Task<int> CountAsync([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///查询指定的查询。
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="parameters">参数。</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> Query([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        ///查询异步。
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="parameters">参数。</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> QueryAsync([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        ///查询指定的查询。
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="parameters">参数。</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TAny> Query<TAny>([NotNull] string query, [CanBeNull] object parameters = null) where TAny : class;

        /// <summary>
        ///查询指定的查询。
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="parameters">参数。</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TAny>> QueryAsync<TAny>([NotNull] string query, [CanBeNull] object parameters = null) where TAny : class;

        /// <summary>
        ///执行给定的查询文本
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="parameters">参数。</param>
        /// <returns></returns>
        int Execute([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        ///以异步方式执行给定的查询文本
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="parameters">参数。</param>
        Task<int> ExecuteAsync([NotNull] string query, [CanBeNull] object parameters = null);

        /// <summary>
        ///获取集合。
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="firstResult">第一个结果。</param>
        /// <param name="maxResults">最大结果。</param>
        /// <param name="sortingProperty">排序属性。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetSet([NotNull] Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, [NotNull] string sortingProperty, bool ascending = true);

        /// <summary>
        ///     Gets the set.
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="firstResult">第一个结果。</param>
        /// <param name="maxResults">最大结果。</param>
        /// <param name="sortingProperty">排序属性。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        IEnumerable<TEntity> GetSet([NotNull] Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        /// <summary>
        ///     Gets the set asynchronous.
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="firstResult">第一个结果。</param>
        /// <param name="maxResults">最大结果。</param>
        /// <param name="sortingProperty">排序属性。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetSetAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, [NotNull] string sortingProperty, bool ascending = true);

        /// <summary>
        ///     Gets the set asynchronous.
        /// </summary>
        /// <param name="predicate">谓语</param>
        /// <param name="firstResult">第一个结果。</param>
        /// <param name="maxResults">最大结果。</param>
        /// <param name="sortingProperty">排序属性。</param>
        /// <param name="ascending">如果设置为<c>true</c>[升序]。</param>
        /// <returns></returns>
        [NotNull]
        Task<IEnumerable<TEntity>> GetSetAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, [NotNull] params Expression<Func<TEntity, object>>[] sortingExpression);

        /// <summary>
        ///插入指定的实体。
        /// </summary>
        /// <param name="entity">实体</param>
        void Insert([NotNull] TEntity entity);

        /// <summary>
        /// 插入和获取标识符。
        /// </summary>
        /// <param name="entity">实体</param>
        TPrimaryKey InsertAndGetId([NotNull] TEntity entity);

        /// <summary>
        ///插入异步。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [NotNull]
        Task InsertAsync([NotNull] TEntity entity);

        /// <summary>
        ///异步插入和获取标识符。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [NotNull]
        Task<TPrimaryKey> InsertAndGetIdAsync([NotNull] TEntity entity);

        /// <summary>
        ///更新指定的实体。
        /// </summary>
        /// <param name="entity">实体</param>
        void Update([NotNull] TEntity entity);

        /// <summary>
        ///更新异步。
        /// </summary>
        /// <param name="entity">实体</param>
        [NotNull]
        Task UpdateAsync([NotNull] TEntity entity);

        /// <summary>
        ///删除指定的实体。
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete([NotNull] TEntity entity);

        /// <summary>
        ///删除指定的实体。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        void Delete([NotNull] Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///删除指定的实体。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [NotNull]
        Task DeleteAsync([NotNull] TEntity entity);

        /// <summary>
        ///删除异步。
        /// </summary>
        /// <param name="predicate">谓语。</param>
        /// <returns></returns>
        [NotNull]
        Task DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
    }
}
