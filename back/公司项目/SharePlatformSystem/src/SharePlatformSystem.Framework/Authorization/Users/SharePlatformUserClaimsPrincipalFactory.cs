using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharePlatformSystem.Auth.EfRepository.Core;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Framework.Authorization.Users;

namespace SharePlatformSystem.Core.Authorization.Users
{
    public class SharePlatformUserClaimsPrincipalFactory<TUser> : UserClaimsPrincipalFactory<TUser>, ITransientDependency
        where TUser : Entity
    {
        public SharePlatformUserClaimsPrincipalFactory(
            SharePlatformUserManager<TUser> userManager,
            IOptions<IdentityOptions> optionsAccessor
            ) : base(userManager, optionsAccessor)
        {

        }

        [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await base.CreateAsync(user);       

            return principal;
        }
    }
}