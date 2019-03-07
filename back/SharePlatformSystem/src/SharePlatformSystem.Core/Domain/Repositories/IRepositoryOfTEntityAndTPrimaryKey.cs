using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Core.Domain.Repositories
{
    /// <summary>
    /// 此接口由所有存储库实现，以确保固定方法的实现。
    /// </summary>
    /// <typeparam name="TEntity">此存储库工作的主要实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体的主键类型</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Select/Get/Query

        /// <summary>
        ///用于获取用于从整个表中检索实体的IQueryable。
        /// </summary>
        /// <returns>IQueryable用于从数据库中选择实体</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        ///用于获取用于从整个表中检索实体的IQueryable。
        ///或一个或多个
        /// </summary>
        /// <param name="propertySelectors">包含表达式的列表。</param>
        /// <returns>IQueryable用于从数据库中选择实体</returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        /// <summary>
        /// 用于获取所有实体。
        /// </summary>
        /// <returns>所有实体的列表</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 用于获取所有实体。
        /// </summary>
        /// <returns>用于获取所有实体。</returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        ///用于获取基于给定“谓词”的所有实体。
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns>所有实体的列表</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于获取基于给定“谓词”的所有实体。
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns>所有实体的列表</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于对整个实体运行查询。
        ///“unitofworkattribute”属性并非总是必需的（与getall相反）
        ///如果“querymethod”使用tolist、firstordefault等完成iqueryable。
        /// </summary>
        /// <typeparam name="T">此方法的返回值类型</typeparam>
        /// <param name="queryMethod">此方法用于查询实体</param>
        /// <returns>查询结果</returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        /// <summary>
        ///获取具有给定主键的实体。
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体</returns>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// 获取具有给定主键的实体。
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体</returns>
        Task<TEntity> GetAsync(TPrimaryKey id);

        /// <summary>
        ///只获取一个具有给定谓词的实体。
        ///如果没有实体或多个实体，则引发异常。
        /// </summary>
        /// <param name="predicate">实体</param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///只获取一个具有给定谓词的实体。
        ///如果没有实体或多个实体，则引发异常。
        /// </summary>
        /// <param name="predicate">实体</param>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取具有给定主键的实体，如果找不到，则为空。
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体或零</returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        ///获取具有给定主键的实体，如果找不到，则为空。
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体或零</returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        /// <summary>
        ///获取具有给定谓词的实体，如果找不到，则为空。
        /// </summary>
        /// <param name="predicate">筛选实体的谓词</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取具有给定谓词的实体，如果找不到，则为空。
        /// </summary>
        /// <param name="predicate">筛选实体的谓词</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///使用给定的主键创建一个不具有数据库访问权限的实体。
        /// </summary>
        /// <param name="id">要加载的实体的主键</param>
        /// <returns>实体</returns>
        TEntity Load(TPrimaryKey id);

        #endregion

        #region Insert

        /// <summary>
        ///插入新实体。
        /// </summary>
        /// <param name="entity">插入的实体</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入新实体。
        /// </summary>
        /// <param name="entity">插入的实体</param>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        ///插入新实体并获取其ID。
        ///可能需要保存当前工作单位
        ///以便能够检索ID。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体的ID</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);

        /// <summary>
        ///插入新实体并获取其ID。
        ///可能需要保存当前工作单位
        ///以便能够检索ID。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体的ID</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        /// <summary>
        /// 根据ID的值插入或更新给定的实体。
        /// </summary>
        /// <param name="entity">实体</param>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>
        /// 根据ID的值插入或更新给定的实体。
        /// </summary>
        /// <param name="entity">实体</param>
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        /// <summary>
        ///根据ID的值插入或更新给定的实体。
        ///还返回实体的ID。
        ///可能需要保存当前工作单位
        ///以便能够检索ID。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体的ID</returns>
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);

        /// <summary>
        ///根据ID的值插入或更新给定的实体。
        ///还返回实体的ID。
        ///可能需要保存当前工作单位
        ///以便能够检索ID。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体的ID</returns>
        Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

        #endregion

        #region Update

        /// <summary>
        ///更新现有实体。
        /// </summary>
        /// <param name="entity">实体</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新现有实体。
        /// </summary>
        /// <param name="entity">实体</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新现有实体。
        /// </summary>
        /// <param name="id">实体的ID</param>
        /// <param name="updateAction">可用于更改实体值的操作</param>
        /// <returns>更新实体</returns>
        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        /// <summary>
        ///更新现有实体。
        /// </summary>
        /// <param name="id">实体的ID</param>
        /// <param name="updateAction">可用于更改实体值的操作</param>
        /// <returns>更新实体</returns>
        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        #endregion

        #region Delete

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 按主键删除实体。
        /// </summary>
        /// <param name="id">实体的主键</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 按主键删除实体。
        /// </summary>
        /// <param name="id">实体的主键</param>
        Task DeleteAsync(TPrimaryKey id);

        /// <summary>
        ///按函数删除多个实体。
        ///注意：所有符合给定谓词的实体都将被检索和删除。
        ///如果有太多的实体具有
        ///给定谓词。
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///按函数删除多个实体。
        ///注意：所有符合给定谓词的实体都将被检索和删除。
        ///如果有太多的实体具有
        ///给定谓词。
        /// </summary>
        /// <param name="predicate"筛选实体的条件</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregates

        /// <summary>
        //获取此存储库中所有实体的计数。
        /// </summary>
        /// <returns>实体计数</returns>
        int Count();

        /// <summary>
        ///获取此存储库中所有实体的计数。
        /// </summary>
        /// <returns>实体计数</returns>
        Task<int> CountAsync();

        /// <summary>
        ///基于给定的“谓词”获取此存储库中所有实体的计数。
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体计数</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///基于给定的“谓词”获取此存储库中所有实体的计数。
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体计数</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///获取此存储库中所有实体的计数（如果预期的返回值大于“int.maxvalue”，则使用）。
        /// </summary>
        /// <returns>实体计数</returns>
        long LongCount();

        /// <summary>
        ///获取此存储库中所有实体的计数（如果预期的返回值大于“int.maxvalue”，则使用）。
        /// </summary>
        /// <returns>实体计数</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 基于给定的“谓词”获取此存储库中所有实体的计数
        ///（如果预期的返回值大于“int.maxvalue”，则使用此重载）。
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体计数</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///基于给定的“谓词”获取此存储库中所有实体的计数
        ///（如果预期的返回值大于“int.maxvalue”，则使用此重载）。
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体计数</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
