using System;
using System.Collections.Generic;

namespace SharePlatformSystem.EntityHistory
{
    /// <summary>
    /// ��������ʵ����ʷ��¼��
    /// </summary>
    public interface IEntityHistoryConfiguration
    {
        /// <summary>
        ///��������/����ʵ����ʷ��¼ϵͳ��
        ///Ĭ��ֵ���档����Ϊfalse����ȫ��������
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        ///�����ǰ�û�δ��¼��������Ϊtrue�����ñ���ʵ����ʷ��¼��
        ///Ĭ��ֵ��false��
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// ����ѡ��Ӧ��ΪĬ�ϸ��ٵ���/�ӿڵ�ѡ�����б�
        /// </summary>
        IEntityHistorySelectorList Selectors { get; }

        /// <summary>
        /// ��������ʵ����ʷ��¼���ٵ����л����͡�
        /// </summary>
        List<Type> IgnoredTypes { get; }
    }
}
