using System.Collections.Generic;
using System.Globalization;

namespace SharePlatformSystem.core.Localization.Dictionaries
{
    /// <summary>
    /// ��ʾ���ڲ��ұ��ػ��ַ������ֵ䡣
    /// </summary>
    public interface ILocalizationDictionary
    {
        /// <summary>
        /// �ֵ���Ļ���
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        ///��ȡ/���þ��и������ƣ������Ĵ��ֵ���ַ�����
        /// </summary>
        /// <param name="name">��ȡ/��������</param>
        string this[string name] { get; set; }

        /// <summary>
        /// ��ȡ�����ġ�name���ġ�localizedstring����
        /// </summary>
        /// <param name="name">��ȡ���ػ��ַ��������ƣ�����</param>
        /// <returns>���ػ��ַ���������ڴ˴ʵ����Ҳ�������Ϊ�ա�</returns>
        LocalizedString GetOrNull(string name);

        /// <summary>
        /// ��ȡ���ֵ��������ַ������б�
        /// </summary>
        /// <returns>List of all <see cref="LocalizedString"/> ����</returns>
        IReadOnlyList<LocalizedString> GetAllStrings();
    }
}