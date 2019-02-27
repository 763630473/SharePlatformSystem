using System;
using Microsoft.EntityFrameworkCore;

namespace SharePlatformSystem.Core.Configuration
{
    public interface ISharePlatformEfCoreConfiguration
    {
        void AddDbContext<TDbContext>(Action<SharePlatformDbContextConfiguration<TDbContext>> action)
            where TDbContext : DbContext;
    }
}
