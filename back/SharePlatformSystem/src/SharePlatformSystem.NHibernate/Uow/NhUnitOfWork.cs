using System.Data.Common;
using System.Threading.Tasks;
using NHibernate;
using SharePlatformSystem.Core.Domain.Uow;
using SharePlatformSystem.Core.Transactions.Extensions;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Domain.Uow;

namespace SharePlatformSystem.NHibernate.Uow
{
    /// <summary>
    /// 执行NHibernate的工作单元。
    /// </summary>
    public class NhUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        ///获取执行查询的nhibernate会话对象。
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// <see cref="NhUnitOfWork"/>如果设置了此数据库连接，则使用它。
        ///这通常在测试中设置。
        /// </summary>
        public DbConnection DbConnection { get; set; }

        private readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        /// <summary>
        ///创建<see cref=“nhUnitOfWork”/>的新实例。
        /// </summary>
        public NhUnitOfWork(
            ISessionFactory sessionFactory,
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkDefaultOptions defaultOptions,
            IUnitOfWorkFilterExecuter filterExecuter)
            : base(
                  connectionStringResolver,
                  defaultOptions,
                  filterExecuter)
        {
            _sessionFactory = sessionFactory;
        }

        protected override void BeginUow()
        {
            Session = DbConnection != null
                ? _sessionFactory.WithOptions().Connection(DbConnection).OpenSession()
                : _sessionFactory.OpenSession();

            if (Options.IsTransactional == true)
            {
                _transaction = Options.IsolationLevel.HasValue
                    ? Session.BeginTransaction(Options.IsolationLevel.Value.ToSystemDataIsolationLevel())
                    : Session.BeginTransaction();
            }

            CheckAndSetSoftDelete();
        }

        protected virtual void CheckAndSetSoftDelete()
        {
            if (IsFilterEnabled(SharePlatformDataFilters.SoftDelete))
            {
                ApplyEnableFilter(SharePlatformDataFilters.SoftDelete); //Enable Filters
                ApplyFilterParameterValue(SharePlatformDataFilters.SoftDelete, SharePlatformDataFilters.Parameters.IsDeleted, false); //ApplyFilter
            }
            else
            {
                ApplyDisableFilter(SharePlatformDataFilters.SoftDelete); //Disable filters
            }
        }       

        public override void SaveChanges()
        {
            Session.Flush();
        }

        public override Task SaveChangesAsync()
        {
            Session.Flush();
            return Task.FromResult(0);
        }

        /// <summary>
        ///提交事务并关闭数据库连接。
        /// </summary>
        protected override void CompleteUow()
        {
            SaveChanges();
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        protected override Task CompleteUowAsync()
        {
            CompleteUow();
            return Task.FromResult(0);
        }

        /// <summary>
        ///回滚事务并关闭数据库连接。
        /// </summary>
        protected override void DisposeUow()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            Session.Dispose();
        }
    }
}