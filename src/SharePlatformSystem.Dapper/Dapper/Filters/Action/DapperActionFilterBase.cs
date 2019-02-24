using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Runtime.Session;
using SharePlatformSystem.Core.Reflection;
namespace SharePlatformSystem.Dapper.Filters.Action
{
    public abstract class DapperActionFilterBase
    {
        protected DapperActionFilterBase()
        {
            SharePlatformSession = NullSharePlatformSession.Instance;
            GuidGenerator = SequentialGuidGenerator.Instance;
        }

        public ISharePlatformSession SharePlatformSession { get; set; }

        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }

        public IGuidGenerator GuidGenerator { get; set; }

        protected virtual TPrimaryKey GetAuditUserId<TPrimaryKey>()
        {
            if (!string.IsNullOrWhiteSpace(SharePlatformSession.UserId) && CurrentUnitOfWorkProvider?.Current != null)
            {
                return (TPrimaryKey)Convert.ChangeType(SharePlatformSession.UserId, typeof(TPrimaryKey)); 
            }

            return default(TPrimaryKey);
        }

        protected virtual void CheckAndSetId(object entityAsObj)
        {
            var entity = entityAsObj as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                Type entityType = entityAsObj.GetType();
                PropertyInfo idProperty = entityType.GetProperty("Id");
                var dbGeneratedAttr = ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
                if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }
    }
}
