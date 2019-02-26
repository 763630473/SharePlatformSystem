using System;
using SharePlatformSystem.Core.Auditing.Entities;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntityDto : FullAuditedEntityDto<string>
    {

    }

    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IFullAudited{TUser}"/> interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAudited<TPrimaryKey>
    {
        /// <summary>
        /// Is this entity deleted?
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Deleter user's Id, if this entity is deleted,
        /// </summary>
        public TPrimaryKey DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time, if this entity is deleted,
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}