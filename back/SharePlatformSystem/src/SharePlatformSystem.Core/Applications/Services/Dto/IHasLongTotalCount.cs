namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// �˽ӿڶ���Ϊ��׼����������Ŀ����������Ϊ�����͵�DTO��
    /// </summary>
    public interface IHasLongTotalCount
    {
        /// <summary>
        ///��Ŀ������
        /// </summary>
        long TotalCount { get; set; }
    }
}