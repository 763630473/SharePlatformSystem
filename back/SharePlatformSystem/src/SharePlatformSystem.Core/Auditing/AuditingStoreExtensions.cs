using SharePlatformSystem.Threading;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// “IAuditingStore”的扩展方法。
    /// </summary>
    public static class AuditingStoreExtensions
    {
        /// <summary>
        /// 应将审核保存到持久存储。
        /// </summary>
        /// <param name="auditingStore">审计存储</param>
        /// <param name="auditInfo">审计信息</param>
        public static void Save(this IAuditingStore auditingStore, AuditInfo auditInfo)
        {
            AsyncHelper.RunSync(() => auditingStore.SaveAsync(auditInfo));
        }
    }
}