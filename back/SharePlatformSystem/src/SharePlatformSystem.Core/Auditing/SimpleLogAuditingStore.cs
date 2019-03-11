using System.Threading.Tasks;
using Castle.Core.Logging;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    ///实现“IAuditingStore”以简单地将审核写入日志。
    /// </summary>
    public class SimpleLogAuditingStore : IAuditingStore
    {
        /// <summary>
        /// 单例实例。
        /// </summary>
        public static SimpleLogAuditingStore Instance { get; } = new SimpleLogAuditingStore();

        public ILogger Logger { get; set; }

        public SimpleLogAuditingStore()
        {
            Logger = NullLogger.Instance;
        }

        public Task SaveAsync(AuditInfo auditInfo)
        {
            if (auditInfo.Exception == null)
            {
                Logger.Info(auditInfo.ToString());
            }
            else
            {
                Logger.Warn(auditInfo.ToString());
            }

            return Task.FromResult(0);
        }
    }
}