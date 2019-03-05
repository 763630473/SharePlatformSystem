using Castle.MicroKernel.Registration;
using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Core.Orm;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.NHibernate
{
    public class NhBasedSecondaryOrmRegistrar : ISecondaryOrmRegistrar, ITransientDependency
    {
        public string OrmContextKey => SharePlatformConsts.Orms.NHibernate;

        public void RegisterRepositories(IIocManager iocManager, AutoRepositoryTypesAttribute defaultRepositoryTypes)
        {
            if (defaultRepositoryTypes.WithDefaultRepositoryInterfaces)
            {
                iocManager.IocContainer.Register(
                    Component.For(typeof(IRepository<>),defaultRepositoryTypes.RepositoryInterface).ImplementedBy(defaultRepositoryTypes.RepositoryImplementation).LifestyleTransient(),
                    Component.For(typeof(IRepository<,>),defaultRepositoryTypes.RepositoryInterfaceWithPrimaryKey).ImplementedBy(defaultRepositoryTypes.RepositoryImplementationWithPrimaryKey).LifestyleTransient()
                );
            }
            else
            {
                iocManager.Register(defaultRepositoryTypes.RepositoryInterface, defaultRepositoryTypes.RepositoryImplementation, DependencyLifeStyle.Transient);
                iocManager.Register(defaultRepositoryTypes.RepositoryInterfaceWithPrimaryKey, defaultRepositoryTypes.RepositoryImplementationWithPrimaryKey, DependencyLifeStyle.Transient);
            }
        }
    }
}
