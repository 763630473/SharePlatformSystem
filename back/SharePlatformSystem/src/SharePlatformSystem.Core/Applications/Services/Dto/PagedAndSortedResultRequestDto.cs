using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 只需实现“ipagedAndSortedResultRequest”。
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string Sorting { get; set; }
    }
}