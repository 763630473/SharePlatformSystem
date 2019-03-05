using DapperExtensions.Sql;
using FluentNHibernate.Cfg.Db;
using NHibernate.Connection;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.NHibernate.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace SharePlatformSystem.NHibernate.DBConnectionBuilder
{
    public class SharePlatformOracleConnection : IDBConncetion
    {
        private readonly object _lockObject = new object();
        private OracleConnection _connection;
        private string _assemblyName;
        private string _connectionString;
        public SharePlatformOracleConnection(string connectionStr, string assemblyName)
        {
            _assemblyName = assemblyName;
            _connectionString = connectionStr;
            _connection = new OracleConnection(connectionStr);
        }
        public void Open()
        {
            _connection.Open();
            lock (_lockObject)
            {
                DapperExtensions.DapperExtensions.SqlDialect = new OracleDialect();
            }
            var Configuration = IocManager.Instance.Resolve<ISharePlatformStartupConfiguration>();
            var assemblyPath = Path.Combine(AppContext.BaseDirectory, _assemblyName + ".dll");
            Configuration.Modules.SharePlatformNHibernate().FluentConfiguration
                    .Database(OracleClientConfiguration.Oracle10.ConnectionString(_connectionString).Provider<DriverConnectionProvider>().Driver<OracleManagedDataClientDriver>())
                    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.LoadFile(assemblyPath)))
                    .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, _connection, Console.Out));

        }
    }
}
