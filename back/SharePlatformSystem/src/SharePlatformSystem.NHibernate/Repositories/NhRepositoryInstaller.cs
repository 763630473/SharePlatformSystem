﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using SharePlatformSystem.Core.Domain.Repositories;

namespace SharePlatformSystem.NHibernate.Repositories.Repositories
{
    internal class NhRepositoryInstaller : IWindsorInstaller
    {
        private readonly ISessionFactory _sessionFactory;

        public NhRepositoryInstaller(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISessionFactory>().Instance(_sessionFactory).LifeStyle.Singleton,
                Component.For(typeof(IRepository<>)).ImplementedBy(typeof(NhRepositoryBase<>)).LifestyleTransient(),
                Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NhRepositoryBase<,>)).LifestyleTransient()
            );
        }
    }
}
