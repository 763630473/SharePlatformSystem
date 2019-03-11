using SharePlatformSystem.Core.Exceptions;

namespace SharePlatformSystem.Runtime.Session
{
    /// <summary>
    /// <see cref=“ishareplatformsession”/>的扩展方法。
    /// </summary>
    public static class SharePlatformSessionExtensions
    {
        /// <summary>
        ///获取当前用户的ID。
        ///throws<see cref=“shareplatformException”/>if<see cref=“ishareplatformsession.userid”/>为空。
        /// </summary>
        /// <param name="session">会话对象。</param>
        /// <returns>当前用户的ID。</returns>
        public static string GetUserId(this ISharePlatformSession session)
        {
            if (string.IsNullOrWhiteSpace(session.UserId))
            {
                throw new SharePlatformException("session.userid为空！可能用户没有登录。");
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