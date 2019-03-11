namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口定义为标准化以请求分页和排序的结果。
    /// </summary>
    public interface IPagedAndSortedResultRequest : IPagedResultRequest, ISortedResultRequest
    {
        
    }
}