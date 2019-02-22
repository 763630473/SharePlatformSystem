using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    /// 对于完全审计的实体，此接口将“ideletionaudied”标记为“iaudied”。
    /// </summary>
    public interface IFullAudited<TPrimaryKey> : IAudited<TPrimaryKey>, IDeletionAudited<TPrimaryKey>
    {

    }

    /// <summary>
    /// 将导航属性添加到用户的“ifullAudited”界面。
    /// </summary>
    /// <typeparam name="TUser">用户实体的类型</typeparam>
    public interface IFullAudited<TUser, TPrimaryKey> : IAudited<TUser, TPrimaryKey>, IFullAudited<TPrimaryKey>, IDeletionAudited<TUser, TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {

    }
}