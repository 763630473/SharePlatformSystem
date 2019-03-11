namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口定义为标准化以请求有限的结果。
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        ///最大预期结果计数。
        /// </summary>
        int MaxResultCount { get; set; }
    }
}