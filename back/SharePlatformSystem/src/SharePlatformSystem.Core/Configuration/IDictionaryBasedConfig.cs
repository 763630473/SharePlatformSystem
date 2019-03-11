using System;

namespace SharePlatformSystem.Core.Configuration
{
    /// <summary>
    /// ����ʹ���ֵ�������õĽӿڡ�
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// ����������Ϊconfiguration���ַ�����
        ///����Ѿ����ھ�����ͬ��name�������ã������������ǡ�
        /// </summary>
        /// <param name="name">���õ�Ψһ����</param>
        /// <param name="value">���õ�ֵ</param>
        /// <returns>���ش��ݵ�ֵ</returns>
        void Set<T>(string name, T value);

        /// <summary>
        /// ��ȡ���и������Ƶ����ö���
        /// </summary>
        /// <param name="name">���õ�Ψһ����</param>
        /// <returns>���õ�ֵ������Ҳ�������Ϊ��</returns>
        object Get(string name);

        /// <summary>
        /// ��ȡ���и������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">���������</typeparam>
        /// <param name="name">���õ�Ψһ����</param>
        /// <returns>���õ�ֵ������Ҳ�������Ϊ��</returns>
        T Get<T>(string name);

        /// <summary>
        /// ��ȡ���и������Ƶ����ö���
        /// </summary>
        /// <param name="name">���õ�Ψһ����</param>
        /// <param name="defaultValue">����Ҳ����������ã���Ϊ�����Ĭ��ֵ</param>
        /// <returns>���õ�ֵ������Ҳ�������Ϊ��</returns>
        object Get(string name, object defaultValue);

        /// <summary>
        ///��ȡ���и������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">���������</typeparam>
        /// <param name="name">���õ�Ψһ����</param>
        /// <param name="defaultValue">����Ҳ����������ã���Ϊ�����Ĭ��ֵ</param>
        /// <returns>���õ�ֵ������Ҳ�������Ϊ��</returns>
        T Get<T>(string name, T defaultValue);
        /// <summary>
        /// ��ȡ���и������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">���������</typeparam>
        /// <param name="name">���õ�Ψһ����</param>
        /// <param name="creator">����Ҳ������������ã��������Դ����ĺ���</param>
        /// <returns>���õ�ֵ������Ҳ�������Ϊ��</returns>
        T GetOrCreate<T>(string name, Func<T> creator);
    }
}