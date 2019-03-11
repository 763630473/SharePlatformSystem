using SharePlatformSystem.Logging;
using SharePlatformSystem.Threading;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 实现ISettingStore的默认行为。
    ///只实现了“getsettingornullasync”方法，并获取设置的值
    ///如果应用程序的配置文件存在，则返回空值。
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        /// <summary>
        /// 获取singleton实例。
        /// </summary>
        public static DefaultConfigSettingStore Instance { get; } = new DefaultConfigSettingStore();
        private DefaultConfigSettingStore()
        {
        }

        public Task<SettingInfo> GetSettingOrNullAsync(string userId, string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                return Task.FromResult<SettingInfo>(null);
            }

            return Task.FromResult(new SettingInfo(userId, name, value));

        }
        public Task DeleteAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support DeleteAsync.");
            return SharePlatformTaskCache.CompletedTask;
        }

        public Task CreateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support CreateAsync.");
            return SharePlatformTaskCache.CompletedTask;
        }

        public Task UpdateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support UpdateAsync.");
            return SharePlatformTaskCache.CompletedTask;
        }

        public Task<List<SettingInfo>> GetAllListAsync(string userId)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support GetAllListAsync.");
            return Task.FromResult(new List<SettingInfo>());
        }
    }
}