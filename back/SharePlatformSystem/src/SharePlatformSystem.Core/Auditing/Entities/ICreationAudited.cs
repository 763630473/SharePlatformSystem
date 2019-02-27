using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    ///此接口由想要存储创建信息的实体（创建人和创建时间）实现。
    ///将实体保存到数据库时，会自动设置创建时间和创建者用户。
    /// </summary>
    public interface ICreationAudited<TPrimaryKey> : IHasCreationTime 
    {
        /// <summary>
        /// 此实体的创建者用户的ID。
        /// </summary>
        TPrimaryKey CreatorUserId { get; set; }
    }

    /// <summary>
    /// 将导航属性添加到用户的ICreationaudited接口。
    /// </summary>
    /// <typeparam name="TUser">用户实体的类型</typeparam>
    public interface ICreationAudited<TUser, TPrimaryKey> : ICreationAudited<TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 引用此实体的创建者用户。
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}