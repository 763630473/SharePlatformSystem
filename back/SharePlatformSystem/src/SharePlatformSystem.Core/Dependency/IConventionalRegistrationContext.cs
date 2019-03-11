using System.Reflection;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 用于在常规注册过程中传递所需对象。
    /// </summary>
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        ///获取正在注册的程序集。
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 引用IOC容器以注册类型。
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// 注册配置。
        /// </summary>
        ConventionalRegistrationConfig Config { get; }
    }
}