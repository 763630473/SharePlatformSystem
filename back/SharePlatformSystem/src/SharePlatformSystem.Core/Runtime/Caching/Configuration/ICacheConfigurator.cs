using System;

namespace SharePlatformSystem.Runtime.Caching.Configuration
{
    /// <summary>
    /// ��ע��Ļ������ó���
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        ///��������ơ�
        ///������������������л��棬��Ϊ�ա�
        /// </summary>
        string CacheName { get; }

        /// <summary>
        ///���ò������ڴ���������������á�
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}