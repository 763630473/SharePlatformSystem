using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using SharePlatformSystem.Core.Collections.Extensions;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.Runtime.Caching;
using SharePlatformSystem.Runtime.Session;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 此类实现“IsettingManager”来管理数据库中的设置值。
    /// </summary>
    public class SettingManager : ISettingManager, ISingletonDependency
    {
        public const string ApplicationSettingsCacheKey = "ApplicationSettings";

        /// <summary>
        /// 对当前会话的引用。
        /// </summary>
        public ISharePlatformSession SharePlatformSession { get; set; }

        /// <summary>
        /// 对设置存储的引用。
        /// </summary>
        public ISettingStore SettingStore { get; set; }

        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly ITypedCache<string, Dictionary<string, SettingInfo>> _applicationSettingCache;
        private readonly ITypedCache<string, Dictionary<string, SettingInfo>> _userSettingCache;

        /// <inheritdoc/>
        public SettingManager(ISettingDefinitionManager settingDefinitionManager, ICacheManager cacheManager)
        {
            _settingDefinitionManager = settingDefinitionManager;

            SharePlatformSession = NullSharePlatformSession.Instance;
            SettingStore = DefaultConfigSettingStore.Instance;

            _applicationSettingCache = cacheManager.GetApplicationSettingsCache();
            _userSettingCache = cacheManager.GetUserSettingsCache();
        }

        #region Public methods

        public Task<string> GetSettingValueAsync(string name)
        {
            return GetSettingValueInternalAsync(name, SharePlatformSession.UserId);
        }

        public Task<string> GetSettingValueForApplicationAsync(string name)
        {
            return GetSettingValueInternalAsync(name);
        }

        public Task<string> GetSettingValueForApplicationAsync(string name, bool fallbackToDefault)
        {
            return GetSettingValueInternalAsync(name, fallbackToDefault: fallbackToDefault);
        }

        public Task<string> GetSettingValueForTenantAsync(string name)
        {
            return GetSettingValueInternalAsync(name);
        }

        public Task<string> GetSettingValueForTenantAsync(string name, bool fallbackToDefault)
        {
            return GetSettingValueInternalAsync(name, fallbackToDefault: fallbackToDefault);
        }

        public Task<string> GetSettingValueForUserAsync(string name, string userId)
        {
            return GetSettingValueInternalAsync(name,userId);
        }

        public Task<string> GetSettingValueForUserAsync(string name, string userId, bool fallbackToDefault)
        {
            return GetSettingValueInternalAsync(name,userId, fallbackToDefault);
        }

        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync()
        {
            return await GetAllSettingValuesAsync(SettingScopes.Application | SettingScopes.User);
        }

        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync(SettingScopes scopes)
        {
            var settingDefinitions = new Dictionary<string, SettingDefinition>();
            var settingValues = new Dictionary<string, ISettingValue>();

            //用默认值填充所有设置。
            foreach (var setting in _settingDefinitionManager.GetAllSettingDefinitions())
            {
                settingDefinitions[setting.Name] = setting;
                settingValues[setting.Name] = new SettingValueObject(setting.Name, setting.DefaultValue);
            }

            //覆盖应用程序设置
            if (scopes.HasFlag(SettingScopes.Application))
            {
                foreach (var settingValue in await GetAllSettingValuesForApplicationAsync())
                {
                    var setting = settingDefinitions.GetOrDefault(settingValue.Name);

                    //TODO: 条件变得复杂，试着简化它
                    if (setting == null || !setting.Scopes.HasFlag(SettingScopes.Application))
                    {
                        continue;
                    }

                    if (!setting.IsInherited &&
                        setting.Scopes.HasFlag(SettingScopes.User) && !string.IsNullOrWhiteSpace(SharePlatformSession.UserId))
                    {
                        continue;
                    }

                    settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
                }
            }

            //覆盖用户设置
            if (scopes.HasFlag(SettingScopes.User) &&!string.IsNullOrWhiteSpace( SharePlatformSession.UserId))
            {
                foreach (var settingValue in await GetAllSettingValuesForUserAsync(SharePlatformSession.ToUserIdentifier()))
                {
                    var setting = settingDefinitions.GetOrDefault(settingValue.Name);
                    if (setting != null && setting.Scopes.HasFlag(SettingScopes.User))
                    {
                        settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
                    }
                }
            }

            return settingValues.Values.ToImmutableList();
        }

        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForApplicationAsync()
        {
            return (await GetApplicationSettingsAsync()).Values
                .Select(setting => new SettingValueObject(setting.Name, setting.Value))
                .ToImmutableList();
        }

        public Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForUserAsync(string userId)
        {
            return GetAllSettingValuesForUserAsync(new UserIdentifier(userId));
        }

        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForUserAsync(UserIdentifier user)
        {
            return (await GetReadOnlyUserSettings(user)).Values
                .Select(setting => new SettingValueObject(setting.Name, setting.Value))
                .ToImmutableList();
        }

        [UnitOfWork]
        public virtual async Task ChangeSettingForApplicationAsync(string name, string value)
        {
            await InsertOrUpdateOrDeleteSettingValueAsync(name, value, null);
            await _applicationSettingCache.RemoveAsync(ApplicationSettingsCacheKey);
        }

        [UnitOfWork]
        public virtual async Task ChangeSettingForTenantAsync(string name, string value)
        {
            await InsertOrUpdateOrDeleteSettingValueAsync(name, value, null);
        }

        [UnitOfWork]
        public virtual Task ChangeSettingForUserAsync(string userId, string name, string value)
        {
            return ChangeSettingForUserAsync(new UserIdentifier(userId), name, value);
        }

        public async Task ChangeSettingForUserAsync(UserIdentifier user, string name, string value)
        {
            await InsertOrUpdateOrDeleteSettingValueAsync(name, value,user.UserId);
            await _userSettingCache.RemoveAsync(user.ToUserIdentifierString());
        }

        #endregion

        #region Private methods

        private async Task<string> GetSettingValueInternalAsync(string name, string userId = null, bool fallbackToDefault = true)
        {
            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(name);

            //为用户获取（如果已定义）
            if (settingDefinition.Scopes.HasFlag(SettingScopes.User) && !string.IsNullOrWhiteSpace(userId))
            {
                var settingValue = await GetSettingValueForUserOrNullAsync(new UserIdentifier(userId), name);
                if (settingValue != null)
                {
                    return settingValue.Value;
                }

                if (!fallbackToDefault)
                {
                    return null;
                }

                if (!settingDefinition.IsInherited)
                {
                    return settingDefinition.DefaultValue;
                }
            }

            //获取应用程序（如果定义）
            if (settingDefinition.Scopes.HasFlag(SettingScopes.Application))
            {
                var settingValue = await GetSettingValueForApplicationOrNullAsync(name);
                if (settingValue != null)
                {
                    return settingValue.Value;
                }

                if (!fallbackToDefault)
                {
                    return null;
                }
            }

            //未定义，获取默认值
            return settingDefinition.DefaultValue;
        }

        private async Task<SettingInfo> InsertOrUpdateOrDeleteSettingValueAsync(string name, string value,string userId)
        {
            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(name);
            var settingValue = await SettingStore.GetSettingOrNullAsync(userId, name);

            //确定默认值
            var defaultValue = settingDefinition.DefaultValue;

            if (settingDefinition.IsInherited)
            {              
                var applicationValue = await GetSettingValueForApplicationOrNullAsync(name);
                if (applicationValue != null)
                {
                    defaultValue = applicationValue.Value;
                }             
            }

            //如果该值是默认值，则不需要存储在数据库上
            if (value == defaultValue)
            {
                if (settingValue != null)
                {
                    await SettingStore.DeleteAsync(settingValue);
                }

                return null;
            }

            //如果它不是默认值且未存储在数据库中，则插入它
            if (settingValue == null)
            {
                settingValue = new SettingInfo
                {
                    UserId = userId,
                    Name = name,
                    Value = value
                };

                await SettingStore.CreateAsync(settingValue);
                return settingValue;
            }

            //它在数据库中的值相同，不需要更新
            if (settingValue.Value == value)
            {
                return settingValue;
            }

            //更新数据库上的设置。
            settingValue.Value = value;
            await SettingStore.UpdateAsync(settingValue);

            return settingValue;
        }

        private async Task<SettingInfo> GetSettingValueForApplicationOrNullAsync(string name)
        {
            return (await GetApplicationSettingsAsync()).GetOrDefault(name);
        }

        private async Task<SettingInfo> GetSettingValueForUserOrNullAsync(UserIdentifier user, string name)
        {
            return (await GetReadOnlyUserSettings(user)).GetOrDefault(name);
        }

        private async Task<Dictionary<string, SettingInfo>> GetApplicationSettingsAsync()
        {
            return await _applicationSettingCache.GetAsync(ApplicationSettingsCacheKey, async () =>
            {
                var dictionary = new Dictionary<string, SettingInfo>();

                var settingValues = await SettingStore.GetAllListAsync(null);
                foreach (var settingValue in settingValues)
                {
                    dictionary[settingValue.Name] = settingValue;
                }

                return dictionary;
            });
        }

        private async Task<ImmutableDictionary<string, SettingInfo>> GetReadOnlyUserSettings(UserIdentifier user)
        {
            var cachedDictionary = await GetUserSettingsFromCache(user);
            lock (cachedDictionary)
            {
                return cachedDictionary.ToImmutableDictionary();
            }
        }

        private async Task<Dictionary<string, SettingInfo>> GetUserSettingsFromCache(UserIdentifier user)
        {
            return await _userSettingCache.GetAsync(
                user.ToUserIdentifierString(),
                async () =>
                {
                    var dictionary = new Dictionary<string, SettingInfo>();

                    var settingValues = await SettingStore.GetAllListAsync(user.UserId);
                    foreach (var settingValue in settingValues)
                    {
                        dictionary[settingValue.Name] = settingValue;
                    }

                    return dictionary;
                });
        }

        public Task<string> GetSettingValueForUserAsync(string name, UserIdentifier user)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(user, nameof(user));

            return GetSettingValueForUserAsync(name, user.UserId);
        }

        #endregion

        #region Nested classes

        private class SettingValueObject : ISettingValue
        {
            public string Name { get; private set; }

            public string Value { get; private set; }

            public SettingValueObject(string name, string value)
            {
                Value = value;
                Name = name;
            }
        }

        #endregion
    }
}