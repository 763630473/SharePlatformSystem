using System.Reflection;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 这个类用于在常规注册过程中传递所需的对象。
    /// </summary>
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        /// <summary>
        ///获取正在注册的程序集。
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// 引用IOC容器以注册类型。
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// 注册配置。
        /// </summary>
        public ConventionalRegistrationConfig Config { get; private set; }

        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}