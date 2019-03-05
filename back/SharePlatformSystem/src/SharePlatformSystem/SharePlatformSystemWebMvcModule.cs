using DapperExtensions.Sql;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NHibernate.Connection;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using SharePlatformSystem.Auth.EfRepository;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Modules;
using SharePlatformSystem.Dapper;
using SharePlatformSystem.Framework.AspNetCore;
using SharePlatformSystem.NetHouse.App;
using SharePlatformSystem.NHibernate;
using SharePlatformSystem.NHibernate.Configuration.Startup;
using SharePlatformSystem.NHibernate.DBConnectionBuilder;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection;


namespace SharePlatformSystem.Web
{
    [DependsOn(
        typeof(SharePlatformKernelModule),
        typeof(SharePlatformSystemInfrastructureModule),
        typeof(SharePlatformSystemAuthAppModule),
                typeof(SharePlatformSystemNetHouseAppModule),
        typeof(SharePlatformAspNetCoreModule),
          typeof(SharePlatformAspNetCoreModule)
        ,
         typeof(SharePlatformNHibernateModule),
        typeof(SharePlatformDapperModule)
        )]
    public class SharePlatformSystemWebMvcModule : SharePlatformModule
    {
        private readonly IHostingEnvironment _env;
        private readonly object _lockObject = new object();
        public SharePlatformSystemWebMvcModule(
    IHostingEnvironment env)
        {
            _env = env;           
        }

        public override void PreInitialize()
        {
            //IocManager.Register<IAuth, LocalAuth>();
            //var builder = new DbContextOptionsBuilder<SharePlatformDBContext>();

            var connections = IocManager.Resolve<IDictionary<string, IDictionary<SqlType, string>>>();
            foreach (var con in connections)
            {
                foreach (var c in con.Value)
                {
                    var connectionContext = new DbConnectionFactory(c.Key,c.Value,con.Key);
                    connectionContext.DBConncetionContext.OpenConncetion();
                }
                
            }
            //lock (_lockObject)
            //{
            //    DapperExtensions.DapperExtensions.SqlDialect = new OracleDialect();
            //}
            //foreach(var connectionDict in connections)
            //{
            //    var assemblyPath = Path.Combine(AppContext.BaseDirectory, connectionDict.Key+ ".dll");
            //    Configuration.Modules.SharePlatformNHibernate().FluentConfiguration
            //            .Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionDict.Value.ConnectionString).Provider<DriverConnectionProvider>().Driver<OracleManagedDataClientDriver>())
            //           .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.LoadFile(assemblyPath)))
            //           .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, (DbConnection)connectionDict.Value, Console.Out));

            //}


            //Configuration.ReplaceService<IConnectionStringResolver, BidConnectionStringResolver>();
            Configuration.UnitOfWork.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
            Configuration.Auditing.IsEnabled = true;
        }

        public override void Initialize()
        {      
            
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());         
        }
       
        public override void PostInitialize()
        {       
            var workManager = IocManager.Resolve<Threading.BackgroundWorkers.IBackgroundWorkerManager>();         
        }
    }
}
