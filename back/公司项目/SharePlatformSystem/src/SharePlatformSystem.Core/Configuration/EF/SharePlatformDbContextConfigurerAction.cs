using System;
using Microsoft.EntityFrameworkCore;

namespace SharePlatformSystem.Core.Configuration
{
    public class SharePlatformDbContextConfigurerAction<TDbContext> : ISharePlatformDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        public Action<SharePlatformDbContextConfiguration<TDbContext>> Action { get; set; }

        public SharePlatformDbContextConfigurerAction(Action<SharePlatformDbContextConfiguration<TDbContext>> action)
        {
            Action = action;
        }

        public void Configure(SharePlatformDbContextConfiguration<TDbContext> configuration)
        {
            Action(configuration);
        }
    }
}