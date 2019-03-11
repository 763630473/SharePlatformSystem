using System.ComponentModel.DataAnnotations;

namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// ֻ��ʵ�֡�iMitedResultRequest��
    /// </summary>
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 10;
    }
}