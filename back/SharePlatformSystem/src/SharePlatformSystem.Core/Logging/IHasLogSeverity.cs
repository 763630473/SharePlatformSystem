namespace SharePlatformSystem.Logging
{
    /// <summary>
    /// ����<see cref="Severity"/> ���ԵĽӿ� ��μ� (see <see cref="LogSeverity"/>).
    /// </summary>
    public interface IHasLogSeverity
    {
        /// <summary>
        /// ��־�����ԡ�
        /// </summary>
        LogSeverity Severity { get; set; }
    }
}