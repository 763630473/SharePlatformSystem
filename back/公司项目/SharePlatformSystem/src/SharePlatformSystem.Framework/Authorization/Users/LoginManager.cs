using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Framework.Authorization.Users;

namespace SharePlatformSystem.Core.Authorization.Users
{
    public class LogInManager : SharePlatformLogInManager<User>
    {
        public LogInManager(UserClaimsPrincipalFactory claimsPrincipalFactory) 
            : base(           
                  claimsPrincipalFactory)
        {
        }
    }
}
