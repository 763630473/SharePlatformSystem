using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Extensions;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Core.Orm;
using SharePlatformSystem.Core.Reflection.Extensions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Dapper
{
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformDapperModule : SharePlatformModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactionScopeAvailable = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharePlatformDapperModule).GetAssembly());

            using (IScopedIocResolver scope = IocManager.CreateScope())
            {
                ISecondaryOrmRegistrar[] additionalOrmRegistrars = scope.ResolveAll<ISecondaryOrmRegistrar>();

                foreach (ISecondaryOrmRegistrar registrar in additionalOrmRegistrars)
                {
                    if (registrar.OrmContextKey == SharePlatformConsts.Orms.EntityFramework)
                    {
                        registrar.RegisterRepositories(IocManager, EfBasedDapperAutoRepositoryTypes.Default);
                    }

                    if (registrar.OrmContextKey == SharePlatformConsts.Orms.NHibernate)
                    {
                        registrar.RegisterRepositories(IocManager, NhBasedDapperAutoRepositoryTypes.Default);
                    }

                    if (registrar.OrmContextKey == SharePlatformConsts.Orms.EntityFrameworkCore)
                    {
                        registrar.RegisterRepositories(IocManager, EfBasedDapperAutoRepositoryTypes.Default);
                    }
                }
            }
        }
    }
}
