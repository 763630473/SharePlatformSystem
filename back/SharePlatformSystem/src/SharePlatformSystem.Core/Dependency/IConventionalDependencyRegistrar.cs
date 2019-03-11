namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 此接口用于按约定注册依赖项。
    /// </summary>
    /// <remarks>
    ///实现此接口并注册到“iocmanager.addConventionalRegistrar”方法
    ///根据自己的约定注册类。
    /// </remarks>
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 按约定注册给定程序集的类型。
        /// </summary>
        /// <param name="context">注册上下文</param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}