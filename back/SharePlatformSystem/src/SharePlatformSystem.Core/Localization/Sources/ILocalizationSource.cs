using SharePlatformSystem.Configuration.Startup;
using SharePlatformSystem.core.Localization;
using SharePlatformSystem.Dependency;
using System.Collections.Generic;
using System.Globalization;

namespace SharePlatformSystem.Core.Localization.Sources
{
    /// <summary>
    ///���ػ�Դ���ڻ�ȡ���ػ��ַ�����
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// Դ��Ψһ���ơ�
        /// </summary>
        string Name { get; }

        /// <summary>
        /// �˷����ڵ�һ��ʹ��֮ǰ��SharePlatform���á�
        /// </summary>
        void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver);

        /// <summary>
        ///��ȡ��ǰ�����и������Ƶı��ػ��ַ�����
        ///����ڵ�ǰ���������Ҳ������򷵻�Ĭ�����ԡ�
        /// </summary>
        /// <param name="name">����</param>
        /// <returns>���ػ��ַ���</returns>
        string GetString(string name);

        /// <summary>
        ///��ȡ�������ƺ�ָ�������Եı��ػ��ַ�����
        ///����ڸ��������������Ҳ������򷵻�Ĭ�����ԡ�
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <returns>���ػ��ַ���</returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        ///��ȡ��ǰ�����и������Ƶı��ػ��ַ�����
        ///����Ҳ������򷵻ؿ�ֵ��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="tryDefaults">
        /// ����ڵ�ǰ���������Ҳ������򷵻�Ĭ�����ԡ�
        /// </param>
        /// <returns>���ػ��ַ���</returns>
        string GetStringOrNull(string name, bool tryDefaults = true);

        /// <summary>
        /// ��ȡ�������ƺ�ָ�������Եı��ػ��ַ�����
        ///����Ҳ������򷵻ؿ�ֵ��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <param name="tryDefaults">
        ///true:����ڵ�ǰ���������Ҳ���������˵�Ĭ�����ԡ�
        /// </param>
        /// <returns>���ػ��ַ���</returns>
        string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// ��ȡ��ǰ�����е������ַ�����
        /// </summary>
        /// <param name="includeDefaults">
        ///true:����ڵ�ǰ���������Ҳ���������˵�Ĭ�������ı���
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true);

        /// <summary>
        ///��ȡָ���������е������ַ�����
        /// </summary>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <param name="includeDefaults">
        ///true:����ڵ�ǰ���������Ҳ���������˵�Ĭ�������ı���
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true);
    }
}