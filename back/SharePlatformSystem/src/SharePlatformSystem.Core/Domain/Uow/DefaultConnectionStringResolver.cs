using System.Configuration;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///默认实现“IConnectionStringResolver”。
    ///从“ishareplatformstartupconfiguration”获取连接字符串，
    ///或配置文件中的“默认”连接字符串，
    ///或配置文件中的单个连接字符串。
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        private readonly ISharePlatformStartupConfiguration _configuration;

        /// <summary>
        /// 初始化“DefaultConnectionStringResolver”类的新实例。
        /// </summary>
        public DefaultConnectionStringResolver(ISharePlatformStartupConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            Check.NotNull(args, nameof(args));

            var defaultConnectionString = _configuration.DefaultNameOrConnectionString;
            if (!string.IsNullOrWhiteSpace(defaultConnectionString))
            {
                return defaultConnectionString;
            }

            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            if (ConfigurationManager.ConnectionStrings.Count == 1)
            {
                return ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }

            throw new SharePlatformException("找不到应用程序的连接字符串定义。设置ishareplatformstartupconfiguration.defaultnameorconnectionstring或向application.config文件添加“default”连接字符串。");
        }
    }
}