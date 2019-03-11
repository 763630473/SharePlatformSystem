using System;
using System.Globalization;

namespace SharePlatformSystem.Core.Localization.Sources
{
    /// <summary>
    /// ��ilocalizationsource������չ������
    /// </summary>
    public static class LocalizationSourceExtensions
    {
        /// <summary>
        /// ͨ����ʽ���ַ�����ȡ���ػ��ַ�����
        /// </summary>
        /// <param name="source">���ػ�Դ</param>
        /// <param name="name">����</param>
        /// <param name="args">���ò�����ʽ</param>
        /// <returns>��ʽ���ͱ��ػ��ַ���</returns>
        public static string GetString(this ILocalizationSource source, string name, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name), args);
        }

        /// <summary>
        /// ͨ����ʽ���ַ�������ȡ���������еı��ػ��ַ�����
        /// </summary>
        /// <param name="source">���ػ�Դ</param>
        /// <param name="name">����</param>
        /// <param name="culture">�Ļ�</param>
        /// <param name="args">���ò�����ʽ</param>
        /// <returns>��ʽ���ͱ��ػ��ַ���</returns>
        public static string GetString(this ILocalizationSource source, string name, CultureInfo culture, params object[] args)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Format(source.GetString(name, culture), args);
        }
    }
}