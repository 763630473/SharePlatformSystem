using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    /// 此接口由想要存储删除信息的实体实现（删除的对象和时间）。
    /// </summary>
    public interface IDeletionAudited<TPrimaryKey> : IHasDeletionTime
    {
        /// <summary>
        /// 哪个用户删除了此实体？
        /// </summary>
        TPrimaryKey DeleterUserId { get; set; }
    }

    /// <summary>
    /// 将导航属性添加到用户的“ideletionaudited”界面。
    /// </summary>
    /// <typeparam name="TUser">用户实体的类型</typeparam>
    public interface IDeletionAudited<TUser, TPrimaryKey> : IDeletionAudited<TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {
        /// <summary>
        ///引用此实体的删除程序用户。
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}