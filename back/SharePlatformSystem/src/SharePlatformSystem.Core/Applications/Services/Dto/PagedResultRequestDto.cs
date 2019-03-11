using System;
using System.ComponentModel.DataAnnotations;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 只需实现“ipagedResultRequest”。
    /// </summary>
    [Serializable]
    public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest
    {
        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }
    }
}