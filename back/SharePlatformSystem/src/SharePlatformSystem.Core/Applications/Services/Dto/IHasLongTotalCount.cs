namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口定义为标准化，将“项目总数”设置为长类型的DTO。
    /// </summary>
    public interface IHasLongTotalCount
    {
        /// <summary>
        ///项目总数。
        /// </summary>
        long TotalCount { get; set; }
    }
}