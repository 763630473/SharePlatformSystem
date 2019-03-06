using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.NHibernate.DBConnectionBuilder
{ 
    public class DbConnectionFactory
    {
        public DBConncetionContext DBConncetionContext;
        public DbConnectionFactory(SqlType sqlType,string connectionStr, string assemblyName)
        {
            switch (sqlType)
            {
                case SqlType.MySql:
                    DBConncetionContext = new DBConncetionContext(new SharePlatformMySqlConnection(connectionStr,assemblyName));
                    break;
                case SqlType.Oracle:
                    DBConncetionContext = new DBConncetionContext(new SharePlatformOracleConnection(connectionStr, assemblyName));
                    break;
                default:
                    break;
            }
        }
    }
}
