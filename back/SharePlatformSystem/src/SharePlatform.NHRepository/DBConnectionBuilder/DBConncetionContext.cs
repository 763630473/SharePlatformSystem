using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.NHibernate.DBConnectionBuilder
{
    public class DBConncetionContext
    {
        private IDBConncetion dbConnection;
        public DBConncetionContext(IDBConncetion _dbConnection)
        {
            this.dbConnection = _dbConnection;
        }

        public void OpenConncetion()
        {
            dbConnection.Open();
        }
    }
}
