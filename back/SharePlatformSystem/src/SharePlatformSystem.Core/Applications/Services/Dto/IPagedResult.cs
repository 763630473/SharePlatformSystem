namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 此接口被定义为标准化，以便向客户机返回一页项目。
    /// </summary>
    /// <typeparam name="T">列表中项目的类型</typeparam>
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {

    }
}