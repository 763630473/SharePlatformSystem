using System;
using System.Transactions;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///此属性用于指示声明方法是原子方法，应将其视为工作单元。
    ///截获具有此属性的方法，打开数据库连接，并在调用该方法之前启动事务。
    ///方法调用结束时，提交事务，如果没有异常，则将所有更改应用到数据库，
    ///否则会回滚。
    /// </summary>
    /// <remarks>
    ///如果在调用此方法之前已有一个工作单元，则此属性无效，如果存在，则它使用相同的事务。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 范围选项。
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        ///这是UOW事务性的吗？
        ///如果未提供，则使用默认值。
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        ///UOW超时为毫秒。
        ///如果未提供，则使用默认值。
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        ///如果此UOW是事务性的，则此选项指示事务的隔离级别。
        ///如果未提供，则使用默认值。
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        ///用于防止启动方法的工作单元。
        ///如果已经有启动的工作单元，则忽略此属性。
        ///默认值：false。
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        ///创建新的UnitOfWorkAttribute对象。
        /// </summary>
        public UnitOfWorkAttribute()
        {

        }

        /// <summary>
        /// 创建新的“UnitOfWorkAttribute”对象。
        /// </summary>
        /// <param name="isTransactional">
        /// 此工作单元是否是事务性的？
        /// </param>
        public UnitOfWorkAttribute(bool isTransactional)
        {
            IsTransactional = isTransactional;
        }

        /// <summary>
        ///创建新的“UnitOfWorkAttribute”对象。
        /// </summary>
        /// <param name="timeout">毫秒</param>
        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        ///创建新的“UnitOfWorkAttribute”对象。
        /// </summary>
        /// <param name="isTransactional">此工作单元是否是事务性的？</param>
        /// <param name="timeout">毫秒</param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 创建新的“UnitOfWorkAttribute”对象。
        /// <see cref="IsTransactional"/> 自动设置为真。
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 创建新的“UnitOfWorkAttribute”对象。
        /// <see cref="IsTransactional"/> 自动设置为真。
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <param name="timeout">事务超时（毫秒）</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        ///创建新的“UnitOfWorkAttribute”对象。
        /// <see cref="IsTransactional"/> 自动设置为真。
        /// </summary>
        /// <param name="scope">交易范围</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = true;
            Scope = scope;
        }

        /// <summary>
        /// 创建新的“UnitOfWorkAttribute”对象。
        /// </summary>
        /// <param name="scope">事务范围</param>
        /// <param name="isTransactional">
        /// 此工作单元是否是事务性的？
        /// </param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, bool isTransactional)
        {
            Scope = scope;
            IsTransactional = isTransactional;
        }

        /// <summary>
        ///创建新的“UnitOfWorkAttribute”对象。
        /// <see cref="IsTransactional"/>自动设置为真。
        /// </summary>
        /// <param name="scope">交易范围</param>
        /// <param name="timeout">事务超时（毫秒）</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}