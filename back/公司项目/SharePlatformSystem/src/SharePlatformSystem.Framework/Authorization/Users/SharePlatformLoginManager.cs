using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.Auth.EfRepository.Core;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Framework.Authorization.Users;

namespace SharePlatformSystem.Core.Authorization.Users
{
    public class SharePlatformLogInManager<TUser> : ITransientDependency
        where TUser :Entity
    {

        private readonly SharePlatformUserClaimsPrincipalFactory<TUser> _claimsPrincipalFactory;

        public SharePlatformLogInManager(
            SharePlatformUserClaimsPrincipalFactory<TUser> claimsPrincipalFactory)
        {
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        [UnitOfWork]
        public virtual async Task<SharePlatformLoginResult<TUser>> LoginAsync(UserLoginInfo login)
        {
            var result = await LoginAsyncInternal(login);
            return result;
        }

        protected virtual async Task<SharePlatformLoginResult<TUser>> LoginAsyncInternal(UserLoginInfo login)
        {
            if (login == null || login.LoginProvider.IsNullOrEmpty() || login.ProviderKey.IsNullOrEmpty())
            {
                throw new ArgumentException("login");
            }
            return new SharePlatformLoginResult<TUser>(SharePlatformLoginResultType.UnknownExternalLogin);
        }
        [UnitOfWork]
        public virtual async Task<SharePlatformLoginResult<TUser>> LoginAsync(string userNameOrEmailAddress, string plainPassword, bool shouldLockout = true)
        {
            var result = await LoginAsyncInternal(userNameOrEmailAddress, plainPassword, shouldLockout);
            return result;
        }

        protected virtual async Task<SharePlatformLoginResult<TUser>> LoginAsyncInternal(string userNameOrEmailAddress, string plainPassword, bool shouldLockout)
        {
            if (userNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userNameOrEmailAddress));
            }

            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }



            return new SharePlatformLoginResult<TUser>(SharePlatformLoginResultType.InvalidUserNameOrEmailAddress);
        }

        protected virtual async Task<SharePlatformLoginResult<TUser>> CreateLoginResultAsync(TUser user)
        {       
            var principal = await _claimsPrincipalFactory.CreateAsync(user);

            return new SharePlatformLoginResult<TUser>(
                user,
                principal.Identity as ClaimsIdentity
            );
        }
    }
}
