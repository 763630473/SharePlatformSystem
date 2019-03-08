namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    /// 用于在需要数据库连接时获取连接字符串。
    /// </summary>
    public interface IConnectionStringResolver
    {
        /// <summary>
        ///获取连接字符串名称（在配置文件中）或有效的连接字符串。
        /// </summary>
        /// <param name="args">解析连接字符串时可以使用的参数。</param>
        string GetNameOrConnectionString(ConnectionStringResolveArgs args);
    }
}