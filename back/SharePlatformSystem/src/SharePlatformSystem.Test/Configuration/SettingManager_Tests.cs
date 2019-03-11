using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharePlatformSystem.Runtime.Remoting;
using SharePlatformSystem.Runtime.Session;
using JetBrains.Annotations;
using Shouldly;
using NUnit.Framework;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Runtime.Caching.Memory;
using NSubstitute;
using SharePlatformSystem.TestBase.Runtime.Session;
using SharePlatformSystem.Runtime.Caching.Configuration;

namespace SharePlatformSystem.Tests.Configuration
{
    public class SettingManager_Tests : TestBaseWithLocalIocManager
    {
        private enum MyEnumSettingType
        {
            Setting1 = 0,
            Setting2 = 1,
        }

        private const string MyAppLevelSetting = "MyAppLevelSetting";
        private const string MyAllLevelsSetting = "MyAllLevelsSetting";
        private const string MyNotInheritedSetting = "MyNotInheritedSetting";
        private const string MyEnumTypeSetting = "MyEnumTypeSetting";

        private SettingManager CreateSettingManager(bool multiTenancyIsEnabled = true)
        {
            return new SettingManager(
                CreateMockSettingDefinitionManager(),
                new SharePlatformMemoryCacheManager(
                    LocalIocManager,
                    new CachingConfiguration(Substitute.For<ISharePlatformStartupConfiguration>())
                    ));
        }

        [Test]
        public async Task Should_Get_Default_Values_With_No_Store_And_No_Session()
        {
            var settingManager = CreateSettingManager();

            (await settingManager.GetSettingValueAsync<int>(MyAppLevelSetting)).ShouldBe(42);
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("application level default value");
        }

        [Test]
        public async Task Should_Get_Stored_Application_Value_With_No_Session()
        {
            var settingManager = CreateSettingManager();
            settingManager.SettingStore = new MemorySettingStore();

            (await settingManager.GetSettingValueAsync<int>(MyAppLevelSetting)).ShouldBe(48);
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("tenant 1 stored value");
        }

        [Test]
        public async Task Should_Get_Correct_Values()
        {
            var session = CreateTestSharePlatformSession();

            var settingManager = CreateSettingManager();
            settingManager.SettingStore = new MemorySettingStore();
            settingManager.SharePlatformSession = session;


            //Inherited setting

            session.UserId = "1";
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("user 1 stored value");

            session.UserId = "2";
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("user 2 stored value");

            session.UserId = "3";
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("tenant 1 stored value"); //Because no user value in the store

            session.UserId = "3";
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("tenant 1 stored value"); //Because no user and tenant value in the store

            //Not inherited setting

            session.UserId = "1";

            (await settingManager.GetSettingValueForApplicationAsync(MyNotInheritedSetting)).ShouldBe("application value");
            (await settingManager.GetSettingValueAsync(MyNotInheritedSetting)).ShouldBe("application value");

            (await settingManager.GetSettingValueAsync<MyEnumSettingType>(MyEnumTypeSetting)).ShouldBe(MyEnumSettingType.Setting1);
        }

        [Test]
        public async Task Should_Get_All_Values()
        {
            var settingManager = CreateSettingManager();
            settingManager.SettingStore = new MemorySettingStore();

            (await settingManager.GetAllSettingValuesAsync()).Count.ShouldBe(4);

            (await settingManager.GetAllSettingValuesForApplicationAsync()).Count.ShouldBe(3);         

            (await settingManager.GetAllSettingValuesForUserAsync(new UserIdentifier("1"))).Count.ShouldBe(1);
            (await settingManager.GetAllSettingValuesForUserAsync(new UserIdentifier("2"))).Count.ShouldBe(1);
            (await settingManager.GetAllSettingValuesForUserAsync(new UserIdentifier("3"))).Count.ShouldBe(0);
        }

        [Test]
        public async Task Should_Change_Setting_Values()
        {
            var session = CreateTestSharePlatformSession();

            var settingManager = CreateSettingManager();
            settingManager.SettingStore = new MemorySettingStore();
            settingManager.SharePlatformSession = session;

            //Application level changes

            await settingManager.ChangeSettingForApplicationAsync(MyAppLevelSetting, "53");
            await settingManager.ChangeSettingForApplicationAsync(MyAppLevelSetting, "54");
            await settingManager.ChangeSettingForApplicationAsync(MyAllLevelsSetting, "application level changed value");

            (await settingManager.SettingStore.GetSettingOrNullAsync( null, MyAppLevelSetting)).Value.ShouldBe("54");

            (await settingManager.GetSettingValueAsync<int>(MyAppLevelSetting)).ShouldBe(54);
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("tenant 1 stored value");
            //User level changes

            session.UserId = "1";
            await settingManager.ChangeSettingForUserAsync("1",MyAllLevelsSetting, "user 1 changed value");
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("user 1 changed value");
        }

        [Test]
        public async Task Should_Delete_Setting_Values_On_Default_Value()
        {
            var session = CreateTestSharePlatformSession();
            var store = new MemorySettingStore();

            var settingManager = CreateSettingManager();
            settingManager.SettingStore = store;
            settingManager.SharePlatformSession = session;


            session.UserId = "1";

            //We can get user's personal stored value
            (await store.GetSettingOrNullAsync("1", MyAllLevelsSetting)).ShouldNotBe(null);
           (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("user 1 stored value");

        //    //This will delete setting for the user since it's same as tenant's setting value
              await settingManager.ChangeSettingForUserAsync("1",MyAllLevelsSetting, "tenant 1 stored value");
            (await store.GetSettingOrNullAsync("1", MyAllLevelsSetting)).ShouldBe(null);

        //    //We can get tenant's setting value
           (await store.GetSettingOrNullAsync("1", null)).ShouldBe(null);
           (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("tenant 1 stored value");

        //    //We can get application's value
            (await store.GetSettingOrNullAsync(null, null)).ShouldBe(null);
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("tenant 1 stored value");

        //    //This will delete setting for application since it's same as the default value of the setting
        //    await settingManager.ChangeSettingForApplicationAsync(MyAllLevelsSetting, "application level default value");
            (await store.GetSettingOrNullAsync(null, null)).ShouldBe(null);

        //    //Now, there is no setting value, default value should return
            (await settingManager.GetSettingValueAsync(MyAllLevelsSetting)).ShouldBe("tenant 1 stored value");
        }

        [Test]
        public async Task Should_Save_Application_Level_Setting_As_Tenant_Setting_When_Multi_Tenancy_Is_Disabled()
        {
            // Arrange
            var session = CreateTestSharePlatformSession();

            var settingManager = CreateSettingManager(multiTenancyIsEnabled: false);
            settingManager.SettingStore = new MemorySettingStore();
            settingManager.SharePlatformSession = session;

            // Act
            await settingManager.ChangeSettingForApplicationAsync(MyAllLevelsSetting, "tenant 1 stored value");

            // Assert
            var value = await settingManager.GetSettingValueAsync(MyAllLevelsSetting);
            value.ShouldBe("application level default value");
        }

        [CanBeNull]
        [Test]
        public async Task Should_Get_Tenant_Setting_For_Application_Level_Setting_When_Multi_Tenancy_Is_Disabled()
        {
            // Arrange
            var session = CreateTestSharePlatformSession();

            var settingManager = CreateSettingManager(multiTenancyIsEnabled: false);
            settingManager.SettingStore = new MemorySettingStore();
            settingManager.SharePlatformSession = session;

            // Act
            await settingManager.ChangeSettingForApplicationAsync(MyAllLevelsSetting, "tenant 1 stored value");

            // Assert
            var value = await settingManager.GetSettingValueForApplicationAsync(MyAllLevelsSetting);
            value.ShouldBe("application level default value");
        }

        private static TestSharePlatformSession CreateTestSharePlatformSession()
        {
            return new TestSharePlatformSession(              
                new DataContextAmbientScopeProvider<SessionOverride>(
                    new AsyncLocalAmbientDataContext()
                )
            );
        }

        private static ISettingDefinitionManager CreateMockSettingDefinitionManager()
        {
            var settings = new Dictionary<string, SettingDefinition>
            {
                {MyAppLevelSetting, new SettingDefinition(MyAppLevelSetting, "42")},
                {MyAllLevelsSetting, new SettingDefinition(MyAllLevelsSetting, "application level default value", scopes: SettingScopes.Application | SettingScopes.User)},
                {MyNotInheritedSetting, new SettingDefinition(MyNotInheritedSetting, "default-value", scopes: SettingScopes.Application, isInherited: false)},
                {MyEnumTypeSetting, new SettingDefinition(MyEnumTypeSetting, MyEnumSettingType.Setting1.ToString())},
            };

            var definitionManager = Substitute.For<ISettingDefinitionManager>();

            //Implement methods
            definitionManager.GetSettingDefinition(Arg.Any<string>()).Returns(x => settings[x[0].ToString()]);
            definitionManager.GetAllSettingDefinitions().Returns(settings.Values.ToList());

            return definitionManager;
        }

        private class MemorySettingStore : ISettingStore
        {
            private readonly List<SettingInfo> _settings;

            public MemorySettingStore()
            {
                _settings = new List<SettingInfo>
                {
                    new SettingInfo(null, MyAppLevelSetting, "48"),
                    new SettingInfo(null, MyAllLevelsSetting, "application level stored value"),
                    new SettingInfo( null, MyAllLevelsSetting, "tenant 1 stored value"),
                    new SettingInfo("1", MyAllLevelsSetting, "user 1 stored value"),
                    new SettingInfo("2", MyAllLevelsSetting, "user 2 stored value"),
                    new SettingInfo( null, MyNotInheritedSetting, "application value"),
                };
            }


            public Task<SettingInfo> GetSettingOrNullAsync(string userId, string name)
            {
                return Task.FromResult(_settings.FirstOrDefault(s=>s.UserId == userId && s.Name == name));
            }

#pragma warning disable 1998
            public async Task DeleteAsync(SettingInfo setting)
            {
                _settings.RemoveAll(s =>s.UserId == setting.UserId && s.Name == setting.Name);
            }
#pragma warning restore 1998

#pragma warning disable 1998
            public async Task CreateAsync(SettingInfo setting)
            {
                _settings.Add(setting);
            }
#pragma warning restore 1998

            public async Task UpdateAsync(SettingInfo setting)
            {
                var s = await GetSettingOrNullAsync(setting.UserId, setting.Name);
                if (s != null)
                {
                    s.Value = setting.Value;
                }
            }

            public Task<List<SettingInfo>> GetAllListAsync(string userId)
            {
                return Task.FromResult(_settings.Where(s =>s.UserId == userId).ToList());
            }
        }
    }
}