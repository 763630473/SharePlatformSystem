using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Core.Auditing.Entities
{
    /// <summary>
    /// 此接口由必须审核的实体实现。
    ///保存/更新“实体”对象时自动设置相关属性。
    /// </summary>
    public interface IAudited<TPrimaryKey> : ICreationAudited<TPrimaryKey>, IModificationAudited<TPrimaryKey>
    {

    }

    /// <summary>
    /// 将导航属性添加到用户的“iaudited”界面。
    /// </summary>
    /// <typeparam name="TUser">用户实体的类型</typeparam>
    public interface IAudited<TUser, TPrimaryKey> : IAudited<TPrimaryKey>, ICreationAudited<TUser, TPrimaryKey>, IModificationAudited<TUser, TPrimaryKey>
        where TUser : IEntity<TPrimaryKey>
    {

    }
}