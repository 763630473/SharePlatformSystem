using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 此接口用于从/到数据源（数据库）获取/设置设置设置。
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        ///获取设置或空值。
        /// </summary>
        /// <param name="userId">UserId or null</param>
        /// <param name="name">设置的名称</param>
        /// <returns>设置对象</returns>
        Task<SettingInfo> GetSettingOrNullAsync(string userId, string name);

        /// <summary>
        /// 删除设置。
        /// </summary>
        /// <param name="setting">要删除的设置</param>
        Task DeleteAsync(SettingInfo setting);

        /// <summary>
        ///添加设置。
        /// </summary>
        /// <param name="setting">设置添加</param>
        Task CreateAsync(SettingInfo setting);

        /// <summary>
        /// 更新设置。
        /// </summary>
        /// <param name="setting">设置添加</param>
        Task UpdateAsync(SettingInfo setting);

        /// <summary>
        /// 获取设置列表。
        /// </summary>
        /// <param name="userId">UserId or null</param>
        /// <returns>设置列表</returns>
        Task<List<SettingInfo>> GetAllListAsync(string userId);
    }
}