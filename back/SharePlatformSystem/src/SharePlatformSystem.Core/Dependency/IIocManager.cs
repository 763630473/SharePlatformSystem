using System;
using Castle.Windsor;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// �˽ӿ�����ֱ��ִ��������ע������
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// �� Castle Windsor ���������á�
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// ����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <param name="type">Type to check</param>
        new bool IsRegistered(Type type);

        /// <summary>
        /// ����������֮ǰ�Ƿ���ע�ᡣ
        /// </summary>
        /// <typeparam name="T">���ͼ��</typeparam>
        new bool IsRegistered<T>();
    }
}