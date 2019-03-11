using System.Collections.Generic;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口被定义为标准化，以便向客户机返回项目列表。
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list</typeparam>
    public interface IListResult<T>
    {
        /// <summary>
        /// 项目清单。
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}