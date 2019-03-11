using SharePlatformSystem.Applications.Services.Dto;
using System;
namespace SharePlatformSystem.Notifications
{
    /// <summary>
    /// 表示发送给用户的通知。
    /// </summary>
    [Serializable]
    public class UserNotification : EntityDto<Guid>, IUserIdentifier
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户通知的当前状态。
        /// </summary>
        public UserNotificationState State { get; set; }   
    }
}