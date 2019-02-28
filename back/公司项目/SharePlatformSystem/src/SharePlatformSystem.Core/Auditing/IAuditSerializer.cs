namespace SharePlatformSystem.Auditing
{
    public interface IAuditSerializer
    {
        string Serialize(object obj);
    }
}