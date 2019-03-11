using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharePlatformSystem.EntityHistory
{
    [Table("EntityChanges")]
    public class EntityChange : Entity<string>
    {
        /// <summary>
        /// 属性的最大长度。。
        /// Value: 48.
        /// </summary>
        public const int MaxEntityIdLength = 48;

        /// <summary>
        /// 属性的最大长度。。
        /// Value: 192.
        /// </summary>
        public const int MaxEntityTypeFullNameLength = 192;

        /// <summary>
        /// ChangeTime.
        /// </summary>
        public virtual DateTime ChangeTime { get; set; }

        /// <summary>
        /// ChangeType.
        /// </summary>
        public virtual EntityChangeType ChangeType { get; set; }

        /// <summary>
        /// 获取/设置用于对实体更改进行分组的更改集ID
        /// </summary>
        public virtual long EntityChangeSetId { get; set; }

        /// <summary>
        /// 获取/设置实体的主键。
        /// </summary>
        [StringLength(MaxEntityIdLength)]
        public virtual string EntityId { get; set; }

        /// <summary>
        /// 实体类型的全名。
        /// </summary>
        [StringLength(MaxEntityTypeFullNameLength)]
        public virtual string EntityTypeFullName { get; set; }

        /// <summary>
        /// PropertyChanges.
        /// </summary>
        public virtual ICollection<EntityPropertyChange> PropertyChanges { get; set; }

        #region Not mapped

        [NotMapped]
        public virtual object EntityEntry { get; set; }

        #endregion
    }
}
