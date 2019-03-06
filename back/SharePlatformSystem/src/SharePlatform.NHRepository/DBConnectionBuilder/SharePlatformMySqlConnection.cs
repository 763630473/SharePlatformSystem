using DapperExtensions.Sql;
using FluentNHibernate.Cfg.Db;
using MySql.Data.MySqlClient;
using NHibernate.Connection;
using NHibernate.Tool.hbm2ddl;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.NHibernate.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SharePlatformSystem.NHibernate.DBConnectionBuilder
{
    public class SharePlatformMySqlConnection : IDBConncetion
    {
        private readonly object _lockObject = new object();
        private MySqlConnection _connection;
        private string _assemblyName;
        private string _connectionString;
        public SharePlatformMySqlConnection(string connectionStr,string assemblyName)
        {
            _assemblyName = assemblyName;
            _connectionString = connectionStr;
            _connection = new MySqlConnection(connectionStr);
        }
        public void Open()
        {
            _connection.Open();
            lock (_lockObject)
            {
                DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
            }
            var Configuration = IocManager.Instance.Resolve<ISharePlatformStartupConfiguration>();
            var assemblyPath = Path.Combine(AppContext.BaseDirectory, _assemblyName + ".dll");
            Configuration.Modules.SharePlatformNHibernate().FluentConfiguration
                    .Database(MySQLConfiguration.Standard.ConnectionString(_connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.LoadFile(assemblyPath)))
                    .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, _connection, Console.Out));
        }
    }
}
