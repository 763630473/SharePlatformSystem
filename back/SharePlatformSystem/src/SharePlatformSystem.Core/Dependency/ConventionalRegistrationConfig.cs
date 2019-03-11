using Castle.DynamicProxy;
using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 这个类用于在以常规方式注册类的同时传递配置/选项。
    /// </summary>
    public class ConventionalRegistrationConfig : DictionaryBasedConfig
    {
        /// <summary>
        /// 是否自动安装所有实现。
        ///默认值：真。
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// 创建新的“ConventionalRegistrationConfig”对象。
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}