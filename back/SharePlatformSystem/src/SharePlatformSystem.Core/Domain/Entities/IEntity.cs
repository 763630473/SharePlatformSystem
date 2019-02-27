using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// 定义基本实体类型的接口。系统中的所有实体都必须实现此接口。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体的唯一标识
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// 检查此实体是否是临时的（未持久化到数据库，并且没有ID）。
        /// </summary>
        /// <returns>如果实体已经持久化，则为True</returns>
        bool IsTransient();
    }
}
