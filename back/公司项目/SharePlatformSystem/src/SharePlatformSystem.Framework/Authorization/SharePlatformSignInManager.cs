using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Auth.EfRepository.Core;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Framework.Authorization.Users;
using SharePlatformSystem.Core.Authorization.Users;
using SharePlatformSystem.Core.Authorization;

namespace SharePlatformSystem.Framework.Authorization
{
    public class SharePlatformSignInManager<TUser> : SignInManager<TUser>, ITransientDependency
        where TUser : Entity
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISettingManager _settingManager;
        private readonly AuthenticationOptions _authenticateOptions;

        public SharePlatformSignInManager(
             SharePlatformUserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
             SharePlatformUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IAuthenticationSchemeProvider schemes)
            : base(
                userManager,
                contextAccessor,
                claimsFactory,
                optionsAccessor,
                logger,
                schemes)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _settingManager = settingManager;
        }

        public virtual async Task<SignInResult> SignInOrTwoFactorAsync(SharePlatformLoginResult<TUser> loginResult, bool isPersistent, bool? rememberBrowser = null, string loginProvider = null, bool bypassTwoFactor = false)
        {
            if (loginResult.Result != SharePlatformLoginResultType.Success)
            {
                throw new ArgumentException("loginResult.Result should be success in order to sign in!");
            }

            if (!bypassTwoFactor && IsTrue(" SharePlatform.UserManagement.TwoFactorLogin.IsEnabled"))
            {
                if (await UserManager.GetTwoFactorEnabledAsync(loginResult.User))
                {
                    if ((await UserManager.GetValidTwoFactorProvidersAsync(loginResult.User)).Count > 0)
                    {
                        if (!await IsTwoFactorClientRememberedAsync(loginResult.User) || rememberBrowser == false)
                        {
                            await Context.SignInAsync(
                                IdentityConstants.TwoFactorUserIdScheme,
                                StoreTwoFactorInfo(loginResult.User, loginProvider)
                            );

                            return SignInResult.TwoFactorRequired;
                        }
                    }
                }
            }

            if (loginProvider != null)
            {
                await Context.SignOutAsync(IdentityConstants.ExternalScheme);
            }

            await SignInAsync(loginResult.User, isPersistent, loginProvider);
            return SignInResult.Success;
        }

        public virtual async Task SignOutAndSignInAsync(ClaimsIdentity identity, bool isPersistent)
        {
            await SignOutAsync();
            await SignInAsync(identity, isPersistent);
        }

        public virtual async Task SignInAsync(ClaimsIdentity identity, bool isPersistent)
        {
            await Context.SignInAsync(IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(identity),
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = isPersistent }
            );
        }

        [UnitOfWork]
        public override Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            return base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        protected virtual ClaimsPrincipal StoreTwoFactorInfo(TUser user, string loginProvider)
        {
            var identity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));          

            if (loginProvider != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, loginProvider));
            }

            return new ClaimsPrincipal(identity);
        }    

        public override async Task<bool> IsTwoFactorClientRememberedAsync(TUser user)
        {
            var result = await Context.AuthenticateAsync(IdentityConstants.TwoFactorRememberMeScheme);

            return result?.Principal != null &&
                   result.Principal.FindFirstValue(ClaimTypes.Name) == user.Id.ToString();
        }

        public override async Task RememberTwoFactorClientAsync(TUser user)
        {
            var rememberBrowserIdentity = new ClaimsIdentity(IdentityConstants.TwoFactorRememberMeScheme);

            rememberBrowserIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));       

            await Context.SignInAsync(IdentityConstants.TwoFactorRememberMeScheme,
                new ClaimsPrincipal(rememberBrowserIdentity),
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = true });
        }

        private bool IsTrue(string settingName)
        {
            return  _settingManager.GetSettingValueForApplication<bool>(settingName);
        }
    }
}
