
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Timing;
using System;

namespace SharePlatformSystem.Dapper.Filters.Action
{
    public class DeletionAuditDapperActionFilter : DapperActionFilterBase, IDapperActionFilter
    {
        public void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            if (entity is ISoftDelete)
            {
                var record = entity.As<ISoftDelete>();
                record.IsDeleted = true;
            }

            if (entity is IHasDeletionTime)
            {
                var record = entity.As<IHasDeletionTime>();
                if (record.DeletionTime == null)
                {
                    record.DeletionTime = Clock.Now;
                }
            }

            if (entity is IDeletionAudited<TPrimaryKey>)
            {
                TPrimaryKey userId = GetAuditUserId<TPrimaryKey>();
                var record = entity.As<IDeletionAudited<TPrimaryKey>>();

                if (record.DeleterUserId != null)
                {
                    return;
                }
                if (userId == null)
                {
                    record.DeleterUserId = default(TPrimaryKey);
                    return;
                }         
                record.DeleterUserId = (TPrimaryKey)Convert.ChangeType(userId, typeof(TPrimaryKey)); ;
            }
        }
    }
}
