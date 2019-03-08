using System.Configuration;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Configuration;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.Domain.Uow
{
    /// <summary>
    ///Ĭ��ʵ�֡�IConnectionStringResolver����
    ///�ӡ�ishareplatformstartupconfiguration����ȡ�����ַ�����
    ///�������ļ��еġ�Ĭ�ϡ������ַ�����
    ///�������ļ��еĵ��������ַ�����
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        private readonly ISharePlatformStartupConfiguration _configuration;

        /// <summary>
        /// ��ʼ����DefaultConnectionStringResolver�������ʵ����
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

            throw new SharePlatformException("�Ҳ���Ӧ�ó���������ַ������塣����ishareplatformstartupconfiguration.defaultnameorconnectionstring����application.config�ļ���ӡ�default�������ַ�����");
        }
    }
}