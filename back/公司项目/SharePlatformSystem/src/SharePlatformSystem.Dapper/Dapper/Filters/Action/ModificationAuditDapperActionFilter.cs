using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Timing;

namespace SharePlatformSystem.Dapper.Filters.Action
{
    public class ModificationAuditDapperActionFilter : DapperActionFilterBase, IDapperActionFilter
    {
        public void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            if (entity is IHasModificationTime)
            {
                entity.As<IHasModificationTime>().LastModificationTime = Clock.Now;
            }

            if (entity is IModificationAudited<TPrimaryKey>)
            {
                var record = entity.As<IModificationAudited<TPrimaryKey>>();
                TPrimaryKey userId = GetAuditUserId<TPrimaryKey>();
                if (userId == null)
                {
                    record.LastModifierUserId = default(TPrimaryKey);
                    return;
                }

                record.LastModifierUserId = userId;

            }
        }
    }
}
