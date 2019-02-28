using System.Security.Claims;

namespace SharePlatformSystem.Runtime.Session
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}
