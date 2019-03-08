using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// 一些有用的实体扩展方法。
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// 检查此实体是否为空标记为已删除。
        /// </summary>
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }

        /// <summary>
        ///通过将“isoftDelete.isDeleted”设置为false和
        /// “ideletionaudited”属性为空。
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