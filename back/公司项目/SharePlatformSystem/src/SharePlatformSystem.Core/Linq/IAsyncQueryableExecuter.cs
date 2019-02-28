using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharePlatformSystem.Linq
{
    /// <summary>
    /// This interface is intended to be used by SharePlatform.
    /// </summary>
    public interface IAsyncQueryableExecuter
    {
        Task<int> CountAsync<T>(IQueryable<T> queryable);

        Task<List<T>> ToListAsync<T>(IQueryable<T> queryable);

        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable);
    }
}