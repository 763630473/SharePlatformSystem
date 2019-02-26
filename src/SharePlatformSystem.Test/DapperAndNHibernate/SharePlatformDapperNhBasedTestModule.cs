using System;
using System.Data.Common;
using System.Reflection;
using DapperExtensions.Sql;
using FluentNHibernate.Cfg.Db;
using NHibernate.Connection;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Dapper;
using SharePlatformSystem.NHibernate;
using SharePlatformSystem.NHibernate.Configuration.Startup;
using SharePlatformSystem.TestBase;

namespace SharePlatformSystem.Test.DapperAndNHibernate
{
    [DependsOn(
        typeof(SharePlatformTestBaseModule),
        typeof(SharePlatformNHibernateModule),
        typeof(SharePlatformDapperModule)
        
    )]
    public class SharePlatformDapperNhBasedTestModule : SharePlatformModule
    {
        private readonly object _lockObject = new object();

        public override void PreInitialize()
        {
            var connection = IocManager.Resolve<DbConnection>();
            lock (_lockObject)
            {
                DapperExtensions.DapperExtensions.SqlDialect = new OracleDialect();
            }

            Configuration.Modules.SharePlatformNHibernate().FluentConfiguration
                          .Database(OracleClientConfiguration.Oracle10.ConnectionString(connection.ConnectionString).Provider<DriverConnectionProvider>().Driver<OracleClientDriver>())
                         .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                         .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, connection, Console.Out));
        }
    }
}
