using System;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ֻ��ʵ�֡�ipagedAndSortedResultRequest����
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string Sorting { get; set; }
    }
}