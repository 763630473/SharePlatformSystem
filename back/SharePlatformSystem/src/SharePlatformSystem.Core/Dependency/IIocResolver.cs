using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// Ϊ��Щ���ڽ�����������ඨ��ӿڡ�
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// ��IOC������ȡ����
        ///���صĶ��������ʹ�ú��ͷţ��μ���release������
        /// </summary> 
        /// <typeparam name="T">Ҫ��ȡ�Ķ��������</typeparam>
        /// <returns>����ʵ��</returns>
        T Resolve<T>();

        /// <summary>
        /// ��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���release������
        /// </summary> 
        /// <typeparam name="T">Ҫǿ��ת���Ķ��������</typeparam>
        /// <param name="type">Ҫ�����Ķ��������</param>
        /// <returns>����ʵ��</returns>
        T Resolve<T>(Type type);

        /// <summary>
        ///��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���release������
        /// </summary> 
        /// <typeparam name="T">Ҫ��ȡ�Ķ��������</typeparam>
        /// <param name="argumentsAsAnonymousType">���캯������</param>
        /// <returns>����ʵ��</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        ///��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���release������
        /// </summary> 
        /// <param name="type">Ҫ��ȡ�Ķ��������</param>
        /// <returns>����ʵ��</returns>
        object Resolve(Type type);

        /// <summary>
        ///��IOC������ȡ����
        ///���صĶ�����ʹ�ú�����ͷţ���release������
        /// </summary> 
        /// <param name="type">Ҫ��ȡ�Ķ��������</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>����ʵ��</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// ��ȡ�������͵�����ʵ�֡�
        ///���صĶ�����ʹ�ú�����ͷţ��μ���release������
        /// </summary> 
        /// <typeparam name="T">Ҫ�����Ķ��������</typeparam>
        /// <returns>����ʵ��</returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// ��ȡ�������͵�����ʵ�֡�
        ///���صĶ�����ʹ�ú�����ͷţ��μ���release������
        /// </summary> 
        /// <typeparam name="T">Ҫ�����Ķ��������</typeparam>
        /// <param name="argumentsAsAnonymousType">���캯������</param>
        /// <returns>����ʵ��</returns>
        T[] ResolveAll<T>(object argumentsAsAnonymousType);

        /// <summary>
        ///��ȡ�������͵�����ʵ�֡�
        ///���صĶ�����ʹ�ú�����ͷţ���release������
        /// </summary> 
        /// <param name="type">Ҫ�����Ķ��������</param>
        /// <returns>����ʵ��</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        ///��ȡ�������͵�����ʵ�֡�   
        ///���صĶ�����ʹ�ú�����ͷţ���release������
        /// </summary> 
        /// <param name="type">Ҫ�����Ķ��������</param>
        /// <param name="argumentsAsAnonymousType">���캯������</param>
        /// <returns>����ʵ��</returns>
        object[] ResolveAll(Type type, object argumentsAsAnonymousType);

        /// <summary>
        ///�ͷ�Ԥ�Ƚ����Ķ�����μ����������
        /// </summary>
        /// <param name="obj">Ҫ�ͷŵĶ���</param>
        void Release(object obj);

        /// <summary>
        /// ����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <param name="type">���ͼ��</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// ����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <typeparam name="T">���ͼ��</typeparam>
        bool IsRegistered<T>();
    }
}