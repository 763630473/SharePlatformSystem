using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Core.Authorization.Users;

namespace SharePlatformSystem.Framework.Authorization.Users
{
    public class UserClaimsPrincipalFactory : SharePlatformUserClaimsPrincipalFactory<User>
    {
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  optionsAccessor)
        {
        }
    }
}
