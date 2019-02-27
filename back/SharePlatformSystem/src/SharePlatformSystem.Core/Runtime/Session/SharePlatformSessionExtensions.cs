using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="ISharePlatformSession"/>.
    /// </summary>
    public static class SharePlatformSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="SharePlatformException"/> if <see cref="ISharePlatformSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static string GetUserId(this ISharePlatformSession session)
        {
            if (string.IsNullOrWhiteSpace(session.UserId))
            {
                throw new SharePlatformException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId;
        }

      

        /// <summary>
        /// Creates <see cref="UserIdentifier"/> from given session.
        /// Returns null if <see cref="ISharePlatformSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">The session.</param>
        public static UserIdentifier ToUserIdentifier(this ISharePlatformSession session)
        {
            return session.UserId == null
                ? null
                : new UserIdentifier(session.GetUserId());
        }
    }
}