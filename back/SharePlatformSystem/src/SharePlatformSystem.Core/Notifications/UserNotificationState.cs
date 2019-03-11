namespace SharePlatformSystem.Notifications
{
    /// <summary>
    /// 表示 <see cref="UserNotification"/>的状态.
    /// </summary>
    public enum UserNotificationState
    {
        /// <summary>
        ///用户尚未读取通知。
        /// </summary>
        Unread = 0,

        /// <summary>
        /// 通知由用户读取。
        /// </summary>
        Read
    }
}