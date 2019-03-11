using SharePlatformSystem.Configuration.Startup;

namespace SharePlatformSystem.AutoMapper
{
    /// <summary>
    ///将扩展方法定义为<see cref=“imoduleconfigurations”/>以允许配置shareplatform.automapper模块。
    /// </summary>
    public static class SharePlatformAutoMapperConfigurationExtensions
    {
        /// <summary>
        ///用于配置shareplatform.automapper模块。
        /// </summary>
        public static ISharePlatformAutoMapperConfiguration  SharePlatformAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.SharePlatformConfiguration.Get<ISharePlatformAutoMapperConfiguration> ();
        }
    }
}