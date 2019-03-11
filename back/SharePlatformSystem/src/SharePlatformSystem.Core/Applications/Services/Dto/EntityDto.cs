using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 主键类型是string的快捷实现方式
    /// </summary>
    [Serializable]
    public class EntityDto : EntityDto<string>, IEntityDto
    {
        /// <summary>
        /// 创建一个新的对象
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// 创建一个新的带主键的新对象
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(string id)
            : base(id)
        {
        }
    }

    /// <summary>
    /// 实现基于实体的DTO的公共属性。
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键的类型</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// 实体的主键ID
        /// </summary>
        public TPrimaryKey Id { get; set; }

        /// <summary>
        /// 创建一个新的空实体对象
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// 创建一个新的带主键的实体对象.
        /// </summary>
        /// <param name="id">实体的主键ID</param>
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }
}