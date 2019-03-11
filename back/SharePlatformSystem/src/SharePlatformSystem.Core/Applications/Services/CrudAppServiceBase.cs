using SharePlatformSystem.Applications.Services.Dto;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace SharePlatformSystem.Applications.Services
{
    /// <summary>
    /// 这是CrudappsService和AsyncCrudappsService类的公共基类。
    ///从crudappservice或asyncCrudappservice继承，而不是从此类继承。
    /// </summary>
    public abstract class CrudAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : ApplicationService
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        protected readonly IRepository<TEntity, TPrimaryKey> Repository;

        protected virtual string GetPermissionName { get; set; }

        protected virtual string GetAllPermissionName { get; set; }

        protected virtual string CreatePermissionName { get; set; }

        protected virtual string UpdatePermissionName { get; set; }

        protected virtual string DeletePermissionName { get; set; }

        protected CrudAppServiceBase(IRepository<TEntity, TPrimaryKey> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// 如果需要，应该应用排序。
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="input">输入。</param>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetAllInput input)
        {
            //尝试对查询进行排序（如果可用）
            var sortInput = input as ISortedResultRequest;
            if (sortInput != null)
            {
                if (!sortInput.Sorting.IsNullOrWhiteSpace())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            //任务需要排序，所以如果使用take，我们应该进行排序。
            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }

            //不排序
            return query;
        }

        /// <summary>
        /// 如果需要，应该应用分页。
        /// </summary>
        /// <param name="query">查询。</param>
        /// <param name="input">输入。</param>
        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetAllInput input)
        {
            //尝试使用分页（如果可用）
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }

            //尝试限制查询结果（如果可用）
            var limitedInput = input as ILimitedResultRequest;
            if (limitedInput != null)
            {
                return query.Take(limitedInput.MaxResultCount);
            }

            //无分页
            return query;
        }

        /// <summary>
        /// 此方法应基于给定的输入创建“IQueryable Tentity”。
        /// It should filter query if needed, but should not do sorting or paging.
        /// 排序应在“ApplySorting”中完成，分页应在“ApplyPaging”中完成。
        /// methods.
        /// </summary>
        /// <param name="input">输入。</param>
        protected virtual IQueryable<TEntity> CreateFilteredQuery(TGetAllInput input)
        {
            return Repository.GetAll();
        }

        /// <summary>
        ///将“tenty”映射到“tentitydto”。
        /// 它默认使用“iobjectmapper”。
        /// 它可以为自定义映射覆盖。
        /// </summary>
        protected virtual TEntityDto MapToEntityDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntityDto>(entity);
        }

        /// <summary>
        /// 将“tentitydto”映射到“tentity”以创建新实体。
        /// 它默认使用“iobjectmapper”。
        /// 它可以为自定义映射覆盖。
        /// </summary>
        protected virtual TEntity MapToEntity(TCreateInput createInput)
        {
            return ObjectMapper.Map<TEntity>(createInput);
        }

        /// <summary>
        /// 将“tupdateinput”映射到“tenty”以更新实体。
        /// 它默认使用“iobjectmapper”。
        /// 它可以为自定义映射覆盖。
        /// </summary>
        protected virtual void MapToEntity(TUpdateInput updateInput, TEntity entity)
        {
            ObjectMapper.Map(updateInput, entity);
        }
    }
}
