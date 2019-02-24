using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// Some useful extension methods for Entities.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Check if this Entity is null of marked as deleted.
        /// </summary>
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }

        /// <summary>
        /// Undeletes this entity by setting <see cref="ISoftDelete.IsDeleted"/> to false and
        /// <see cref="IDeletionAudited"/> properties to null.
        /// </summary>
        public static void UnDelete<TPrimaryKey>(this ISoftDelete entity)
        {
            entity.IsDeleted = false;
            if (entity is IDeletionAudited<TPrimaryKey>)
            {
                var deletionAuditedEntity = entity.As<IDeletionAudited<TPrimaryKey>>();
                deletionAuditedEntity.DeletionTime = null;
                deletionAuditedEntity.DeleterUserId = default(TPrimaryKey);
            }
        }
    }
}