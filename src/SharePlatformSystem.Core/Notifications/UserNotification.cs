using SharePlatformSystem.Application.Services.Dto;
using System;
namespace SharePlatformSystem.Notifications
{
    /// <summary>
    /// Represents a notification sent to a user.
    /// </summary>
    [Serializable]
    public class UserNotification : EntityDto<Guid>, IUserIdentifier
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        public UserNotificationState State { get; set; }   
    }
}