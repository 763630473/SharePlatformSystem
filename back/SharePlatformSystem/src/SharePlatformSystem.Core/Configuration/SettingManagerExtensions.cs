using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// “IsettingManager”的扩展方法。
    /// </summary>
    public static class SettingManagerExtensions
    {
        /// <summary>
        /// 获取给定类型（“t”）中设置的值。
        /// </summary>
        /// <typeparam name="T">要获取的设置类型</typeparam>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>设置值</returns>
        public static async Task<T> GetSettingValueAsync<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return (await settingManager.GetSettingValueAsync(name)).To<T>();
        }

        /// <summary>
        /// 获取应用程序级别设置的当前值。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>应用程序设置的当前值</returns>
        public static async Task<T> GetSettingValueForApplicationAsync<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return (await settingManager.GetSettingValueForApplicationAsync(name)).To<T>();
        }

        /// <summary>
        /// 获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="userId">User id</param>
        /// <returns>用户设置的当前值</returns>
        public static async Task<T> GetSettingValueForUserAsync<T>(this ISettingManager settingManager, string name,string userId)
           where T : struct
        {
            return (await settingManager.GetSettingValueForUserAsync(name,userId)).To<T>();
        }

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="user">User</param>
        /// <returns>用户设置的当前值</returns>
        public static async Task<T> GetSettingValueForUserAsync<T>(this ISettingManager settingManager, string name, UserIdentifier user)
           where T : struct
        {
            return (await settingManager.GetSettingValueForUserAsync(name, user)).To<T>();
        }

        /// <summary>
        ///获取设置的当前值。
        ///获取设置值，如果存在则被应用程序和当前用户覆盖。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>用户设置的当前值</returns>
        public static string GetSettingValue(this ISettingManager settingManager, string name)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync(name));
        }

        /// <summary>
        /// 获取应用程序级别设置的当前值。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>应用程序设置的当前值</returns>
        public static string GetSettingValueForApplication(this ISettingManager settingManager, string name)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForApplicationAsync(name));
        }

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="userId">User id</param>
        /// <returns>用户设置的当前值</returns>
        public static string GetSettingValueForUser(this ISettingManager settingManager, string name,string userId)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForUserAsync(name, userId));
        }

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="userId">User id</param>
        /// <param name="fallbackToDefault"></param>
        /// <returns>用户设置的当前值</returns>
        public static string GetSettingValueForUser(this ISettingManager settingManager, string name,string userId, bool fallbackToDefault)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForUserAsync(name,userId, fallbackToDefault));
        }

        /// <summary>
        /// 获取设置的值。
        /// </summary>
        /// <typeparam name="T">要获取的设置类型</typeparam>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>设置值</returns>
        public static T GetSettingValue<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync<T>(name));
        }

        /// <summary>
        /// 获取应用程序级别设置的当前值。
        /// </summary>
        /// <typeparam name="T">要获取的设置类型</typeparam>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>应用程序设置的当前值</returns>
        public static T GetSettingValueForApplication<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForApplicationAsync<T>(name));
        }

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <typeparam name="T">要获取的设置类型</typeparam>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="userId">User id</param>
        /// <returns>用户设置的当前值</returns>
        public static T GetSettingValueForUser<T>(this ISettingManager settingManager, string name, string userId)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForUserAsync<T>(name, userId));
        }

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <typeparam name="T">要获取的设置类型</typeparam>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="user">User</param>
        /// <returns>用户设置的当前值</returns>
        public static T GetSettingValueForUser<T>(this ISettingManager settingManager, string name, UserIdentifier user)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForUserAsync<T>(name, user));
        }

        /// <summary>
        ///获取所有设置的当前值。
        ///获取所有设置值，如果存在，则被应用程序和当前用户覆盖。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <returns>设定值列表</returns>
        public static IReadOnlyList<ISettingValue> GetAllSettingValues(this ISettingManager settingManager)
        {
            return AsyncHelper.RunSync(settingManager.GetAllSettingValuesAsync);
        }

        /// <summary>
        ///获取为应用程序指定的所有设置值的列表。
        ///只返回为应用程序显式设置的设置。
        ///如果使用设置的默认值，则不包括结果列表。
        ///如果要获取所有设置的当前值，请使用“GetAllSettingValues”方法。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <returns>设定值列表</returns>
        public static IReadOnlyList<ISettingValue> GetAllSettingValuesForApplication(this ISettingManager settingManager)
        {
            return AsyncHelper.RunSync(settingManager.GetAllSettingValuesForApplicationAsync);
        }

        /// <summary>
        ///获取为用户指定的所有设置值的列表。
        ///只返回为用户显式设置的设置。
        ///如果没有为用户设置设置的值（例如，如果用户使用默认值），则不包括结果列表。
        ///如果要获取所有设置的当前值，请使用“GetAllSettingValues”方法。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="user">用户获取设置</param>
        /// <returns>用户的所有设置</returns>
        public static IReadOnlyList<ISettingValue> GetAllSettingValuesForUser(this ISettingManager settingManager, UserIdentifier user)
        {
            return AsyncHelper.RunSync(() => settingManager.GetAllSettingValuesForUserAsync(user));
        }

        /// <summary>
        /// 更改应用程序级别的设置。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="value">设置值</param>
        public static void ChangeSettingForApplication(this ISettingManager settingManager, string name, string value)
        {
            AsyncHelper.RunSync(() => settingManager.ChangeSettingForApplicationAsync(name, value));
        }

        /// <summary>
        ///更改用户的设置。
        /// </summary>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="user">User</param>
        /// <param name="value">设置值</param>
        public static void ChangeSettingForUser(this ISettingManager settingManager, UserIdentifier user, string name, string value)
        {
            AsyncHelper.RunSync(() => settingManager.ChangeSettingForUserAsync(user, name, value));
        }
    }
}