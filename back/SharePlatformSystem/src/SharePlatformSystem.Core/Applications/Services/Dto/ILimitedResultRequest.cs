namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// �˽ӿڶ���Ϊ��׼�����������޵Ľ����
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        ///���Ԥ�ڽ��������
        /// </summary>
        int MaxResultCount { get; set; }
    }
}