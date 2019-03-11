using System;
using System.Reflection;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// Ϊ��Щ����ע����������ඨ��ӿڡ�
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// Ϊ����ע�����������ע������
        /// </summary>
        /// <param name="registrar">������ϵע����</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        ///�����г���ע����ע��������򼯵����͡�����ġ�iocmanager.addConventionalRegistrar��������
        /// </summary>
        /// <param name="assembly">Ҫע��ĳ���</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        ///�����г���ע����ע��������򼯵����͡�����ġ�iocmanager.addConventionalRegistrar��������
        /// </summary>
        /// <param name="assembly">Ҫע��ĳ���</param>
        /// <param name="config">��������</param>
        void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config);

        /// <summary>
        /// ������ע��Ϊ��ע�ᡣ
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// ������ע��Ϊ��ע�ᡣ
        /// </summary>
        /// <param name="type">�������</param>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// ������ʵ��ע��һ�����͡�
        /// </summary>
        /// <typeparam name="TType">����ע������</typeparam>
        /// <typeparam name="TImpl">ʵ�ֵ����͡�ttype��</typeparam>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// ������ʵ��ע��һ�����͡�
        /// </summary>
        /// <param name="type">�������</param>
        /// <param name="impl">ʵ�ֵ�����"type"</param>
        /// <param name="lifeStyle">������Ʒ�����ʽ</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// ����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <param name="type">���ͼ��</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// ����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <typeparam name="TType">���ͼ��</typeparam>
        bool IsRegistered<TType>();
    }
}