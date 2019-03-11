using System.Threading.Tasks;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// 这个接口应该由供应商实现，以使审计工作正常进行。
    /// 默认实现为“SimpleLogAuditingStore”。
    /// </summary>
    public interface IAuditingStore
    {
        /// <summary>
        /// 应将审核保存到持久存储。
        /// </summary>
        /// <param name="auditInfo">审计信息</param>
        Task SaveAsync(AuditInfo auditInfo);
    }
}