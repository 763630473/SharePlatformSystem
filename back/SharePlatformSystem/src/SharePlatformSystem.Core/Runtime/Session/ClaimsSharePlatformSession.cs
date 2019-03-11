using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Runtime.Security;

namespace SharePlatformSystem.Runtime.Session
{
    /// <summary>
    /// 实现<see cref=“ishareplatformsession”/>从当前声明获取会话属性。
    /// </summary>
    public class ClaimsSharePlatformSession : SharePlatformSessionBase, ISingletonDependency
    {
        public override string UserId
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.UserId;
                }

                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == SharePlatformClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                return userIdClaim.Value;
            }
        }

        public override string ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == SharePlatformClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return impersonatorUserIdClaim.Value;
            }
        }
 

        protected IPrincipalAccessor PrincipalAccessor { get; }    

        public ClaimsSharePlatformSession(
            IPrincipalAccessor principalAccessor,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
            : base(
                  sessionOverrideScopeProvider)
        {
            PrincipalAccessor = principalAccessor;
        }
    }
}