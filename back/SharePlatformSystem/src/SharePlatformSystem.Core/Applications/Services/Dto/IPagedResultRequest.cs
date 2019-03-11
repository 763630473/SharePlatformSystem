namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口定义为标准化以请求分页结果。
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        /// <summary>
        /// 跳过计数（页面开头）。
        /// </summary>
        int SkipCount { get; set; }
    }
}