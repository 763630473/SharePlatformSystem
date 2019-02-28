using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Orm
{
    public interface ISecondaryOrmRegistrar
    {
        string OrmContextKey { get; }

        void RegisterRepositories(IIocManager iocManager, AutoRepositoryTypesAttribute defaultRepositoryTypes);
    }
}
