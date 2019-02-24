using System;
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Dapper.Filters.Action
{
    public class CreationAuditDapperActionFilter : DapperActionFilterBase, IDapperActionFilter
    {
        public void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            TPrimaryKey userId = GetAuditUserId<TPrimaryKey>();

            CheckAndSetId(entity);

            var entityWithCreationTime = entity as IHasCreationTime;
            if (entityWithCreationTime != null)
            {
                if (entityWithCreationTime.CreationTime == default(DateTime))
                {
                    entityWithCreationTime.CreationTime = Clock.Now;
                }
            }

            if ((userId  is ICreationAudited<TPrimaryKey> )&& (entity is ICreationAudited<TEntity>))
            {
                var record = entity as ICreationAudited<TPrimaryKey>;
                if (record.CreatorUserId == null)
                {                
                    record.CreatorUserId = userId;
                }
            }

            if (entity is IHasModificationTime)
            {
                entity.As<IHasModificationTime>().LastModificationTime = null;
            }

            if (entity is IModificationAudited<TPrimaryKey>)
            {
                var record = entity.As<IModificationAudited<TPrimaryKey>>();
                record.LastModifierUserId = default(TPrimaryKey);
            }
        }
    }
}
