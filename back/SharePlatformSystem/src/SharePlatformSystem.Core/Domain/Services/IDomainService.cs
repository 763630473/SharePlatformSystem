using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Domain.Services
{
    /// <summary>
    /// 此接口必须由所有域服务实现，以按约定标识它们。
    /// </summary>
    public interface IDomainService : ITransientDependency
    {

    }
}