using SharePlatformSystem.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharePlatformSystem.EntityHistory
{
    [Table("EntityPropertyChanges")]
    public class EntityPropertyChange : Entity<string>
    {
        /// <summary>
        ///属性的最大长度。
        /// Value: 96.
        /// </summary>
        public const int MaxPropertyNameLength = 96;

        /// <summary>
        /// 属性的最大长度。.
        /// Value: 512.
        /// </summary>
        public const int MaxValueLength = 512;

        /// <summary>
        ///属性的最大长度。
        /// Value: 512.
        /// </summary>
        public const int MaxPropertyTypeFullNameLength = 192;

        /// <summary>
        /// EntityChangeId.
        /// </summary>
        public virtual long EntityChangeId { get; set; }

        /// <summary>
        /// NewValue.
        /// </summary>
        [StringLength(MaxValueLength)]
        public virtual string NewValue { get; set; }

        /// <summary>
        /// OriginalValue.
        /// </summary>
        [StringLength(MaxValueLength)]
        public virtual string OriginalValue { get; set; }

        /// <summary>
        /// PropertyName.
        /// </summary>
        [StringLength(MaxPropertyNameLength)]
        public virtual string PropertyName { get; set; }

        /// <summary>
        ///序列化的JSON类型<see cref=“newValue”/>and<see cref=“originalValue”/>。
        ///类型的全名。
        /// </summary>
        [StringLength(MaxPropertyTypeFullNameLength)]
        public virtual string PropertyTypeFullName { get; set; }
    }
}
