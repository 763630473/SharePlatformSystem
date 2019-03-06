using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharePlatformSystem.Auth.EfRepository.Core;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Core.Authorization.Users;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Core.Domain.Services;
using SharePlatformSystem.Core.Localization;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Runtime.Caching;
using SharePlatformSystem.Runtime.Session;

namespace SharePlatformSystem.Framework.Authorization.Users
{
    public class SharePlatformUserManager<TUser> : UserManager<TUser>, IDomainService
        where TUser : Entity
    {
        //protected IUserPermissionStore<TUser> UserPermissionStore
        //{
        //    get
        //    {
        //        if (!(Store is IUserPermissionStore<TUser>))
        //        {
        //            throw new AbpException("Store is not IUserPermissionStore");
        //        }

        //        return Store as IUserPermissionStore<TUser>;
        //    }
        //}

        public ILocalizationManager LocalizationManager { get; set; }

        protected string LocalizationSourceName { get; set; }

        public ISharePlatformSession SharePlatformSession { get; set; }


        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICacheManager _cacheManager;
        private readonly ISettingManager _settingManager;
        private readonly IOptions<IdentityOptions> _optionsAccessor;
        protected SharePlatformUserStore<TUser> SharePlatformUserStore { get; }
        public SharePlatformUserManager(
             SharePlatformUserStore<TUser> userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<TUser>> logger,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            ISettingManager settingManager)
            : base(
                userStore,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _cacheManager = cacheManager;
            _settingManager = settingManager;
            _optionsAccessor = optionsAccessor;
            SharePlatformUserStore = userStore;
            LocalizationManager = NullLocalizationManager.Instance;
            LocalizationSourceName = SharePlatformConsts.LocalizationSourceName;
        }

        public override async Task<IdentityResult> CreateAsync(TUser user)
        {
            return await base.CreateAsync(user);
        }

    }
}