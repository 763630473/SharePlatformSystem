namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// 设置提供程序时使用的上下文。
    /// </summary>
    public class SettingDefinitionProviderContext
    {
        public ISettingDefinitionManager Manager { get; }

        internal SettingDefinitionProviderContext(ISettingDefinitionManager manager)
        {
            Manager = manager;
        }
    }
}