using SharePlatformSystem.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SharePlatformSystem.Core.Domain.Entities
{
    /// <summary>
    /// 如果发现了异常但未找到的实体，则会引发此异常。
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : SharePlatformException
    {
        /// <summary>
        ///实体的类型。
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// 实体的ID。
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// 创建新的“EntityNotFoundException”对象。
        /// </summary>
        public EntityNotFoundException()
        {

        }

        /// <summary>
        ///创建新的“EntityNotFoundException”对象。
        /// </summary>
        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        ///创建新的“EntityNotFoundException”对象。
        /// </summary>
        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }

        /// <summary>
        /// 创建新的“EntityNotFoundException”对象。
        /// </summary>
        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        /// <summary>
        /// 创建新的“EntityNotFoundException”对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        public EntityNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        ///创建新的“EntityNotFoundException”对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
