using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Framework.Authorization.Users;

namespace SharePlatformSystem.Framework.Authorization
{
    public class SignInManager : SharePlatformSignInManager<User>
    {
        public SignInManager(
            UserManager userManager, 
            IHttpContextAccessor contextAccessor,
            UserClaimsPrincipalFactory claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<User>> logger,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IAuthenticationSchemeProvider schemes) 
            : base(
                userManager, 
                contextAccessor, 
                claimsFactory, 
                optionsAccessor, 
                logger,
                unitOfWorkManager,
                settingManager,
                schemes)
        {
        }
    }
}
