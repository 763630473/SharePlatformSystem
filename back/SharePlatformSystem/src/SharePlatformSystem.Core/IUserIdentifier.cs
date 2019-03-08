namespace SharePlatformSystem
{
    /// <summary>
    /// 获取用户标识符的接口。
    /// </summary>
    public interface IUserIdentifier
    {
        /// <summary>
        /// 用户的ID。
        /// </summary>
        string UserId { get; }
    }
}