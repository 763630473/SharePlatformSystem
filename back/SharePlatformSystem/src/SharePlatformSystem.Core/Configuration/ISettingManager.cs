using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 这是必须实现的主界面，以便能够加载/更改设置值。
    /// </summary>
    public interface ISettingManager
    {
        /// <summary>
        ///获取设置的当前值。
        ///获取设置值，如果存在则被应用程序、当前租户和当前用户覆盖。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>设置的当前值</returns>
        Task<string> GetSettingValueAsync(string name);

        /// <summary>
        /// 获取应用程序级别设置的当前值。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <returns>应用程序设置的当前值</returns>
        Task<string> GetSettingValueForApplicationAsync(string name);

        /// <summary>
        /// 获取应用程序级别设置的当前值。
        ///如果fallbacktodefault为false，则只从应用程序获取值，如果应用程序没有为设置定义值，则返回空值。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="fallbackToDefault"></param>
        /// <returns>应用程序设置的当前值</returns>
        Task<string> GetSettingValueForApplicationAsync(string name, bool fallbackToDefault);

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="userId">User id</param>
        /// <returns>用户设置的当前值</returns>
        Task<string> GetSettingValueForUserAsync(string name, string userId);

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，如果fallbacktodefault为true，则由给定的租户和用户覆盖。
        ///如果fallbacktodefault为false，则只从用户获取值，如果用户没有为设置定义值，则返回空值。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="userId">用户ID</param>
        /// <param name="fallbackToDefault"></param>
        /// <returns>用户设置的当前值</returns>
        Task<string> GetSettingValueForUserAsync(string name, string userId, bool fallbackToDefault);

        /// <summary>
        ///获取用户级别设置的当前值。
        ///获取设置值，由给定租户和用户覆盖。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="user">User</param>
        /// <returns>用户设置的当前值</returns>
        Task<string> GetSettingValueForUserAsync(string name, UserIdentifier user);

        /// <summary>
        ///获取所有设置的当前值。
        ///获取所有设置值，由应用程序、当前租户（如果存在）和当前用户（如果存在）覆盖。
        /// </summary>
        /// <returns>设定值列表</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync();

        /// <summary>
        ///获取所有设置的当前值。
        ///获取所有设置的默认值，然后按给定范围覆盖。
        /// </summary>
        /// <param name="scopes">要覆盖的一个或多个范围</param>
        /// <returns>设定值列表</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync(SettingScopes scopes);

        /// <summary>
        ///获取为应用程序指定的所有设置值的列表。
        ///只返回为应用程序显式设置的设置。
        ///如果使用设置的默认值，则不包括结果列表。
        ///如果要获取所有设置的当前值，请使用“getAllSettingValuesAsync（）”方法。
        /// </summary>
        /// <returns>List of setting values</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForApplicationAsync();

        /// <summary>
        ///获取为用户指定的所有设置值的列表。
        ///只返回为用户显式设置的设置。   
        ///如果没有为用户设置设置的值（例如，如果用户使用默认值），则不包括结果列表。
        ///如果要获取所有设置的当前值，请使用“getAllSettingValuesAsync（）”方法。
        /// </summary>
        /// <param name="user">用户获取设置</param>
        /// <returns>用户的所有设置</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForUserAsync(UserIdentifier user);

        /// <summary>
        /// 更改应用程序级别的设置。
        /// </summary>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="value">设置值</param>
        Task ChangeSettingForApplicationAsync(string name, string value);

        /// <summary>
        /// 更改用户的设置。
        /// </summary>
        /// <param name="user">UserId</param>
        /// <param name="name">设置的唯一名称</param>
        /// <param name="value">设置值</param>
        Task ChangeSettingForUserAsync(UserIdentifier user, string name, string value);
    }
}
