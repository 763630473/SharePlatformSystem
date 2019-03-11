namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口定义为标准化以请求排序结果。
    /// </summary>
    public interface ISortedResultRequest
    {
        /// <summary>
        ///信息。
        ///应该包括3场A和optionally方向（升序或降序）
        ///含有超过一个逗号分隔的市场（，）。
        //// <summary>
        string Sorting { get; set; }
    }
}