using SharePlatformSystem.Runtime.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Applications.Services
{
    /// <summary>
    ///此类可以用作应用程序服务的基类。
    /// </summary>
    public abstract class ApplicationService : SharePlatformServiceBase, IApplicationService, IAvoidDuplicateCrossCuttingConcerns
    {
        public static string[] CommonPostfixes = { "AppService", "ApplicationService" };

        /// <summary>
        /// 获取当前会话信息。
        /// </summary>
        public ISharePlatformSession SharePlatformSession { get; set; }


        /// <summary>
        /// 获取应用的横切关注点。
        /// </summary>
        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        /// <summary>
        /// 构造器.
        /// </summary>
        protected ApplicationService()
        {
            SharePlatformSession = NullSharePlatformSession.Instance;
        }
    }
}
