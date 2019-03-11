namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// 提供一个接口，在上层提供审计信息。
    /// </summary>
    public interface IAuditInfoProvider
    {
        /// <summary>
        ///调用以填充所需的属性。
        /// </summary>
        /// <param name="auditInfo">Audit info that is partially filled</param>
        void Fill(AuditInfo auditInfo);
    }
}