namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// �˽ӿڶ���Ϊ��׼���������ҳ�����
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        /// <summary>
        /// ����������ҳ�濪ͷ����
        /// </summary>
        int SkipCount { get; set; }
    }
}