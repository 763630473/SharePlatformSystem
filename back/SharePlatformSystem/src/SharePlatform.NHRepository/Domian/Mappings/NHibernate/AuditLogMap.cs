using SharePlatformSystem.NHibernate.EntityMappings;
using SharePlatformSystem.NHRepository.Entities;

namespace SharePlatformSystem.NHRepository.Mappings.NHibernate
{
    public class AuditLogMap : EntityMap<AuditLog, string>
    {
        public AuditLogMap()
            : base("AuditLogs")
        {
            Map(x => x.UserId);
            Map(x => x.ServiceName);
            Map(x => x.MethodName);
            Map(x => x.Parameters);
            Map(x => x.ExecutionTime);
            Map(x => x.ExecutionDuration);
            Map(x => x.ClientIpAddress);
            Map(x => x.ClientName);
            Map(x => x.BrowserInfo);
            Map(x => x.Exception);
            Map(x => x.ImpersonatorUserId);
            Map(x => x.CustomData);
        }
    }
}