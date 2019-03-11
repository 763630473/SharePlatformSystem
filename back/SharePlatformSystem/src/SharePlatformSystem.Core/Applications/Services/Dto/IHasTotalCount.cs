namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口定义为标准化，将“项目总数”设置为DTO。
    /// </summary>
    public interface IHasTotalCount
    {
        /// <summary>
        /// 项目总数。
        /// </summary>
        int TotalCount { get; set; }
    }
}