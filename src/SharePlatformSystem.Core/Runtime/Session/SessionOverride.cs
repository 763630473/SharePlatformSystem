namespace SharePlatformSystem.Runtime.Session
{
    public class SessionOverride
    {
        public string UserId { get; }

        public SessionOverride(string userId)
        {
            UserId = userId;
        }
    }
}