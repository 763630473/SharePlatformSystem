namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 最常用的主键类型string的快捷方式.
    /// </summary>
    public interface IEntityDto : IEntityDto<string>
    {

    }

    /// <summary>
    /// 为基于实体的DTO定义通用属性。
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// 实体的主键ID
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}