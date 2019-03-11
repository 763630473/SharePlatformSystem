using SharePlatformSystem.Logging;
using SharePlatformSystem.Threading;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// ʵ��ISettingStore��Ĭ����Ϊ��
    ///ֻʵ���ˡ�getsettingornullasync������������ȡ���õ�ֵ
    ///���Ӧ�ó���������ļ����ڣ��򷵻ؿ�ֵ��
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        /// <summary>
        /// ��ȡsingletonʵ����
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