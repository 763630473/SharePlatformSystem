using System;
using System.Data.Common;
using System.Reflection;
using DapperExtensions.Sql;
using FluentNHibernate.Cfg.Db;
using NHibernate.Connection;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.NHibernate;
using SharePlatformSystem.NHibernate.Configuration.Startup;
using SharePlatformSystem.TestBase;

namespace SharePlatformSystem.Test.NHibernate
{
    [DependsOn(typeof(SharePlatformTestBaseModule),typeof(SharePlatformNHibernateModule))]
    public class NHibernateTestModule : SharePlatformModule
    {
        public override void PreInitialize()
        {
            var connection = IocManager.Resolve<DbConnection>();
         
            var cfgs = Configuration.Modules.SharePlatformNHibernate().FluentConfiguration;
            cfgs.Database(OracleClientConfiguration.Oracle10.ConnectionString(connection.ConnectionString).Provider<DriverConnectionProvider>().Driver<OracleManagedDataClientDriver>());
           ;
            cfgs.Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));
            cfgs.ExposeConfiguration(cfg => new SchemaExport(cfg)
            .Execute(true, false, false, connection, Console.Out));
        }
    }
}