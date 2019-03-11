using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// 用于提供配置模块的方法。
    ///创建要在“ishareplatformstartupconfiguration.modules”对象上使用的此类的Entension方法。
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        ///获取SharePlatform配置对象。
        /// </summary>
        ISharePlatformStartupConfiguration SharePlatformConfiguration { get; }
    }
}