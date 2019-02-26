using System.Reflection;
using Abp.NHibernate.Uow;
using NHibernate;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;
using SharePlatformSystem.NHibernate.Configuration;
using SharePlatformSystem.NHibernate.Interceptors;
using SharePlatformSystem.NHibernate.Repositories.Repositories;
using SharePlatformSystem.Core.Configuration.Startup;
using SharePlatformSystem.NHibernate.Filters;
using SharePlatformSystem.NHibernate.Configuration.Startup;
using SharePlatformSystem.NHibernate.Uow;

namespace SharePlatformSystem.NHibernate
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in NHibernate.
    /// </summary>
    [DependsOn(typeof(SharePlatformKernelModule))]
    public class SharePlatformNHibernateModule : SharePlatformModule
    {
        /// <summary>
        /// NHibernate session factory object.
        /// </summary>
        private ISessionFactory _sessionFactory;

        public override void PreInitialize()
        {
            IocManager.Register<ISharePlatformNHibernateModuleConfiguration,SharePlatformNHibernateModuleConfiguration>();
            Configuration.ReplaceService<IUnitOfWorkFilterExecuter, NhUnitOfWorkFilterExecuter>(DependencyLifeStyle.Transient);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.Register<SharePlatformNHibernateInterceptor>(DependencyLifeStyle.Transient);

            _sessionFactory = Configuration.Modules.SharePlatformNHibernate().FluentConfiguration
                .Mappings(m => m.FluentMappings.Add(typeof(SoftDeleteFilter)))
                .ExposeConfiguration(config => config.SetInterceptor(IocManager.Resolve<SharePlatformNHibernateInterceptor>()))
                .BuildSessionFactory();

            IocManager.IocContainer.Install(new NhRepositoryInstaller(_sessionFactory));
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        /// <inheritdoc/>
        public override void Shutdown()
        {
            _sessionFactory.Dispose();
        }
    }
}
