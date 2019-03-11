using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// “iauditinfoProvider”的默认实现。
    /// </summary>
    public class DefaultAuditInfoProvider : IAuditInfoProvider, ITransientDependency
    {
        public IClientInfoProvider ClientInfoProvider { get; set; }

        public DefaultAuditInfoProvider()
        {
            ClientInfoProvider = NullClientInfoProvider.Instance;
        }

        public virtual void Fill(AuditInfo auditInfo)
        {
            if (auditInfo.ClientIpAddress.IsNullOrEmpty())
            {
                auditInfo.ClientIpAddress = ClientInfoProvider.ClientIpAddress;
            }

            if (auditInfo.BrowserInfo.IsNullOrEmpty())
            {
                auditInfo.BrowserInfo = ClientInfoProvider.BrowserInfo;
            }

            if (auditInfo.ClientName.IsNullOrEmpty())
            {
                auditInfo.ClientName = ClientInfoProvider.ComputerName;
            }
        }
    }
}