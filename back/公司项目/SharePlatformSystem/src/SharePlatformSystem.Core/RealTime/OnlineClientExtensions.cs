using JetBrains.Annotations;

namespace SharePlatformSystem.RealTime
{
    public static class OnlineClientExtensions
    {
        [CanBeNull]
        public static UserIdentifier ToUserIdentifierOrNull(this IOnlineClient onlineClient)
        {
            return !string.IsNullOrWhiteSpace(onlineClient.UserId)
                ? new UserIdentifier(onlineClient.UserId)
                : null;
        }
    }
}