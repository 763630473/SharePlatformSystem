using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace SharePlatformSystem.Core.Configuration
{
    public class SharePlatformDbContextConfiguration<TDbContext>
        where TDbContext : DbContext
    {
        public string ConnectionString {get; internal set; }

        public DbConnection ExistingConnection { get; internal set; }

        public DbContextOptionsBuilder<TDbContext> DbContextOptions { get; }
        
        public SharePlatformDbContextConfiguration(string connectionString, DbConnection existingConnection)
        {
            ConnectionString = connectionString;
            ExistingConnection = existingConnection;

            DbContextOptions = new DbContextOptionsBuilder<TDbContext>();
        }
    }
}