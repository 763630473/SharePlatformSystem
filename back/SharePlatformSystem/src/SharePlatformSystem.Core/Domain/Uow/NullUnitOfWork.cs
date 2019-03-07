using System.Threading.Tasks;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///工作单元的实现为空。
    ///如果没有注册组件，则使用它“iunitofwork”。
    ///这样可以确保在没有数据库的情况下工作的SharePlatform。
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public override Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        protected override void BeginUow()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected override Task CompleteUowAsync()
        {
            return Task.FromResult(0);
        }

        protected override void DisposeUow()
        {

        }

        public NullUnitOfWork(
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkDefaultOptions defaultOptions,
            IUnitOfWorkFilterExecuter filterExecuter
            ) : base(
                connectionStringResolver,
                defaultOptions,
                filterExecuter)
        {
        }
    }
}
