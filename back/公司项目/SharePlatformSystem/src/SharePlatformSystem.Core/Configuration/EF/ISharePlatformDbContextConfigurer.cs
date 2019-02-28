using Microsoft.EntityFrameworkCore;

namespace SharePlatformSystem.Core.Configuration
{
    public interface ISharePlatformDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        void Configure(SharePlatformDbContextConfiguration<TDbContext> configuration);
    }
}