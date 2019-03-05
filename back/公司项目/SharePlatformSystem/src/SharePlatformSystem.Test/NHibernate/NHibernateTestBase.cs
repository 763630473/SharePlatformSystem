using System;
using System.Data.Common;
using Castle.MicroKernel.Registration;
using MySql.Data.MySqlClient;
using NHibernate;
using Oracle.ManagedDataAccess.Client;
using SharePlatformSystem.Test.NHibernate.DbConnections;
using SharePlatformSystem.TestBase;

namespace SharePlatformSystem.Test.NHibernate
{
    public class NHibernateTestBase : SharePlatformIntegratedTestBase<NHibernateTestModule>
    {
        private OracleConnection _connection;
        //private MySqlConnection _connection;

        protected override void PreInitialize()
        {
            var _connection = new OracleConnection("User ID=MXWXPT;Password=MXWXPT;Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.182)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = MXWXPT)));persist security info=true;");
            //_connection = new MySqlConnection("Server=localhost; Database =test; User ID=root;Password=123456");
            _connection.Open();
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>().Instance(_connection).LifestyleSingleton()
                );
        }

        public void UsingSession(Action<ISession> action)
        {
            using (var session = LocalIocManager.Resolve<ISessionFactory>().WithOptions().Connection(_connection).OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    action(session);
                    session.Flush();
                    transaction.Commit();
                }
            }
        }

        public T UsingSession<T>(Func<ISession, T> func)
        {
            T result;

            using (var session = LocalIocManager.Resolve<ISessionFactory>().WithOptions().Connection(_connection).OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    result = func(session);
                    session.Flush();
                    transaction.Commit();
                }
            }

            return result;
        }

        public override void Dispose()
        {
            _connection.Dispose();
            base.Dispose();
        }
    }
}