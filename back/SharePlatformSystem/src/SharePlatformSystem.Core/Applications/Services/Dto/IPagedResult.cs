namespace SharePlatformSystem.Applications.Services.Dto
{
    /// <summary>
    /// �˽ӿڱ�����Ϊ��׼�����Ա���ͻ�������һҳ��Ŀ��
    /// </summary>
    /// <typeparam name="T">�б�����Ŀ������</typeparam>
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {

    }
}