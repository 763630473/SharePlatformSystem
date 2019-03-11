using System.Data;

namespace SharePlatformSystem.Core.Data
{
    public interface IActiveTransactionProvider
    {
        /// <summary>
        /// 获取活动事务，如果当前UOW不是事务性的，则为空。
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args);

        /// <summary>
        /// 获取活动的数据库连接。
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args);
    }
}
