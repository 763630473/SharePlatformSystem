using SharePlatformSystem.Runtime.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for application services. 
    /// </summary>
    public abstract class ApplicationService : SharePlatformServiceBase, IApplicationService, IAvoidDuplicateCrossCuttingConcerns
    {
        public static string[] CommonPostfixes = { "AppService", "ApplicationService" };

        /// <summary>
        /// Gets current session information.
        /// </summary>
        public ISharePlatformSession SharePlatformSession { get; set; }
        

        /// <summary>
        /// Gets the applied cross cutting concerns.
        /// </summary>
        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApplicationService()
        {
            SharePlatformSession = NullSharePlatformSession.Instance;
        }
    }
}
