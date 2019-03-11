using SharePlatformSystem.Core.Exceptions;
using System.Data;

namespace SharePlatformSystem.Core.Transactions.Extensions
{
    public static class IsolationLevelExtensions
    {
        /// <summary>
        ///将<see cref=“system.transactions.isolationlevel”/>转换为<see cref=“isolationlevel”/>。
        /// </summary>
        public static IsolationLevel ToSystemDataIsolationLevel(this System.Transactions.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case System.Transactions.IsolationLevel.Chaos:
                    return IsolationLevel.Chaos;
                case System.Transactions.IsolationLevel.ReadCommitted:
                    return IsolationLevel.ReadCommitted;
                case System.Transactions.IsolationLevel.ReadUncommitted:
                    return IsolationLevel.ReadUncommitted;
                case System.Transactions.IsolationLevel.RepeatableRead:
                    return IsolationLevel.RepeatableRead;
                case System.Transactions.IsolationLevel.Serializable:
                    return IsolationLevel.Serializable;
                case System.Transactions.IsolationLevel.Snapshot:
                    return IsolationLevel.Snapshot;
                case System.Transactions.IsolationLevel.Unspecified:
                    return IsolationLevel.Unspecified;
                default:
                    throw new SharePlatformException("未知的隔离级别：" + isolationLevel);
            }
        }
    }
}