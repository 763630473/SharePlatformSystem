using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Core.Domain.Repositories
{
    /// <summary>
    /// “IRepository tenty，TPrimaryKey”的快捷方式，用于大多数使用的主键类型（“string”）。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : class, IEntity<string>
    {

    }
}
