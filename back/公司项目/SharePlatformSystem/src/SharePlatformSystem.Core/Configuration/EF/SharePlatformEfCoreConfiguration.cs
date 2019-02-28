using System;
using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Core.Configuration
{
    public class SharePlatformEfCoreConfiguration : ISharePlatformEfCoreConfiguration
    {
        private readonly IIocManager _iocManager;

        public SharePlatformEfCoreConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public void AddDbContext<TDbContext>(Action<SharePlatformDbContextConfiguration<TDbContext>> action) 
            where TDbContext : DbContext
        {
            _iocManager.IocContainer.Register(
                Component.For<ISharePlatformDbContextConfigurer<TDbContext>>().Instance(
                    new SharePlatformDbContextConfigurerAction<TDbContext>(action)
                ).IsDefault()
            );
        }
    }
}