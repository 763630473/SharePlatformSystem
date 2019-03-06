using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Runtime.Caching;

namespace SharePlatformSystem.Framework.Authorization.Users
{
    public class UserManager : SharePlatformUserManager<User>
    {
        public UserManager(
            UserStore store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, 
            IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<User>> logger, 
            IUnitOfWorkManager unitOfWorkManager, 
            ICacheManager cacheManager, 
            ISettingManager settingManager)
            : base(
                store, 
                optionsAccessor, 
                passwordHasher, 
                userValidators, 
                passwordValidators, 
                keyNormalizer, 
                errors, 
                services, 
                logger, 
                unitOfWorkManager, 
                cacheManager,
                settingManager)
        {
        }
    }
}
