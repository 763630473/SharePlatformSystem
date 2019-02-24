using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.AutoMapper
{
    public class MultiLingualMapContext
    {
        public ISettingManager SettingManager { get; set; }

        public MultiLingualMapContext(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }
    }
}