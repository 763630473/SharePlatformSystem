using SharePlatformSystem.Core.Localization.Sources;

namespace SharePlatformSystem.core.Localization.Dictionaries
{
    /// <summary>
    /// �����ֵ�ı��ػ�Դ�Ľӿڡ�
    /// </summary>
    public interface IDictionaryBasedLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// ��ȡ�ֵ��ṩ����
        /// </summary>
        ILocalizationDictionaryProvider DictionaryProvider { get; }

        /// <summary>
        /// ʹ�ø������ֵ���չԴ��
        /// </summary>
        /// <param name="dictionary">��չԴ���ֵ�</param>
        void Extend(ILocalizationDictionary dictionary);
    }
}