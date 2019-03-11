namespace SharePlatformSystem.Logging
{
    /// <summary>
    /// 定义<see cref="Severity"/> 属性的接口 请参见 (see <see cref="LogSeverity"/>).
    /// </summary>
    public interface IHasLogSeverity
    {
        /// <summary>
        /// 日志严重性。
        /// </summary>
        LogSeverity Severity { get; set; }
    }
}