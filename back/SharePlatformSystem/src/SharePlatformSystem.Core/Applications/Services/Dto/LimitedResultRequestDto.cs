using System.ComponentModel.DataAnnotations;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// 只需实现“iMitedResultRequest”
    /// </summary>
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 10;
    }
}