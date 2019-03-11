using System.Threading.Tasks;

namespace SharePlatformSystem.EntityHistory
{
    /// <summary>
    ///此接口应由供应商实现以
    ///使实体历史记录工作。
    /// </summary>
    public interface IEntityHistoryStore
    {
        /// <summary>
        /// 应将实体更改集保存到持久存储。
        /// </summary>
        /// <param name="entityChangeSet">实体更改集</param>
        Task SaveAsync(EntityChangeSet entityChangeSet);
    }
}
