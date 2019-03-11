using System.Security.Claims;

namespace SharePlatformSystem.Runtime.Security
{
    /// <summary>
    /// 用于获取特定于SharePlatform的声明类型名称。
    /// </summary>
    public static class SharePlatformClaimTypes
    {
        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Name"/>
        /// </summary>
        public static string UserName { get; set; } = ClaimTypes.Name;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.NameIdentifier"/>
        /// </summary>
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Role"/>
        /// </summary>
        public static string Role { get; set; } = ClaimTypes.Role;


        /// <summary>
        /// ImpersonatorUserId.
        /// </summary>
        public static string ImpersonatorUserId { get; set; } = "impersonatorUserId";
    }
}
