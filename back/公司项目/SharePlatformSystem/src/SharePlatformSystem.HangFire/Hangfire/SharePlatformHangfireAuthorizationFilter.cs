using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Runtime.Session;

namespace SharePlatformSystem.HangFire
{
    public class SharePlatformHangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string _requiredPermissionName;

        public SharePlatformHangfireAuthorizationFilter(string requiredPermissionName = null)
        {
            _requiredPermissionName = requiredPermissionName;
        }

        public bool Authorize(DashboardContext context)
        {
            if (!IsLoggedIn(context))
            {
                return false;
            }

            if (!_requiredPermissionName.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }

        private static bool IsLoggedIn(DashboardContext context)
        {
            var sharePlatformSession = context.GetHttpContext().RequestServices.GetRequiredService<ISharePlatformSession>();
            return !string.IsNullOrWhiteSpace(sharePlatformSession.UserId);
        }    
    }
}
