using Castle.DynamicProxy;
using SharePlatformSystem.Core.Configuration;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// ������������Գ��淽ʽע�����ͬʱ��������/ѡ�
    /// </summary>
    public class ConventionalRegistrationConfig : DictionaryBasedConfig
    {
        /// <summary>
        /// �Ƿ��Զ���װ����ʵ�֡�
        ///Ĭ��ֵ���档
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// �����µġ�ConventionalRegistrationConfig������
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}