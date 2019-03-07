using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Domain.Repositories
{
    /// <summary>
    ///此接口必须由所有存储库实现，才能按约定标识它们。
    ///实现通用版本而不是此版本。
    /// </summary>
    public interface IRepository : ITransientDependency
    {
        
    }
}