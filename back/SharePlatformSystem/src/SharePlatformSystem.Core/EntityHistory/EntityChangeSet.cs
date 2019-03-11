using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharePlatformSystem.EntityHistory
{
    [Table("EntityChangeSets")]
    public class EntityChangeSet : Entity<string>, IHasCreationTime, IExtendableObject
    {
        /// <summary>
        /// 属性的最大长度。。
        /// </summary>
        public const int MaxBrowserInfoLength = 512;

        /// <summary>
        /// 属性的最大长度。
        /// </summary>
        public const int MaxClientIpAddressLength = 64;

        /// <summary>
        /// 属性的最大长度。
        /// </summary>
        public const int MaxClientNameLength = 128;

        /// <summary>
        /// 属性的最大长度。
        /// </summary>
        public const int MaxReasonLength = 256;

        /// <summary>
        ///在Web请求中更改此实体时的浏览器信息。
        /// </summary>
        [StringLength(MaxBrowserInfoLength)]
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// 客户端的IP地址。
        /// </summary>
        [StringLength(MaxClientIpAddressLength)]
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端的名称（通常是计算机名称）。
        /// </summary>
        [StringLength(MaxClientNameLength)]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 此实体的创建时间。
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 用于扩展包含对象的JSON格式字符串。
        /// </summary>
        public virtual string ExtensionData { get; set; }

        /// <summary>
        /// ImpersonatorTenantId.
        /// </summary>
        public virtual int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// ImpersonatorUserId.
        /// </summary>
        public virtual long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 此更改集的原因。
        /// </summary>
        [StringLength(MaxReasonLength)]
        public virtual string Reason { get; set; }


        /// <summary>
        /// UserId.
        /// </summary>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 在此更改集中分组的实体更改。
        /// </summary>
        public virtual IList<EntityChange> EntityChanges { get; set; }

        public EntityChangeSet()
        {
            EntityChanges = new List<EntityChange>();
        }
    }
}
