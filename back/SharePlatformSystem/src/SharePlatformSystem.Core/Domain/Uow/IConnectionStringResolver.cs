namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// ��������Ҫ���ݿ�����ʱ��ȡ�����ַ�����
    /// </summary>
    public interface IConnectionStringResolver
    {
        /// <summary>
        ///��ȡ�����ַ������ƣ��������ļ��У�����Ч�������ַ�����
        /// </summary>
        /// <param name="args">���������ַ���ʱ����ʹ�õĲ�����</param>
        string GetNameOrConnectionString(ConnectionStringResolveArgs args);
    }
}