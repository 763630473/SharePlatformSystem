using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Applications.Services
{
    /// <summary>
    /// 此接口必须由所有应用程序服务实现，以按约定标识它们。
    /// </summary>
    public interface IApplicationService : ITransientDependency
    {

    }
}
