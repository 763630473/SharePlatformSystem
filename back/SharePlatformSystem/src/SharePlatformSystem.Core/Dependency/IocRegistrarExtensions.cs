using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// ��IIocRegistrar���ӿڵ���չ������
    /// </summary>
    public static class IocRegistrarExtensions
    {
        #region RegisterIfNot

        /// <summary>
        /// ���������ǰδע�ᣬ����ע��Ϊ��ע�ᡣ
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="iocRegistrar">ע����</param>
        /// <param name="lifeStyle">�����������ڵ�����</param>
        /// <returns>���Ϊ������ʵ��ע�ᣬ��Ϊtrue��</returns>
        public static bool RegisterIfNot<T>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class
        {
            if (iocRegistrar.IsRegistered<T>())
            {
                return false;
            }

            iocRegistrar.Register<T>(lifeStyle);
            return true;
        }

        /// <summary>
        /// ���������ǰδע�ᣬ����ע��Ϊ��ע�ᡣ
        /// </summary>
        /// <param name="iocRegistrar">ע����</param>
        /// <param name="lifeStyle">�����������ڵ�����</param>
        /// <returns>���Ϊ������ʵ��ע�ᣬ��Ϊtrue��</returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, lifeStyle);
            return true;
        }

        /// <summary>
        /// Registers a type with it's implementation if it's not registered before.
        /// </summary>
        /// <typeparam name="TType">ע�������</typeparam>
        /// <typeparam name="TImpl">ʵ�ֵ�����"TType"</typeparam>
        /// <param name="iocRegistrar">ע����</param>
        /// <param name="lifeStyle">�����������ڵ�����</param>
        /// <returns>���Ϊ������ʵ��ע�ᣬ��Ϊtrue��</returns>
        public static bool RegisterIfNot<TType, TImpl>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            if (iocRegistrar.IsRegistered<TType>())
            {
                return false;
            }

            iocRegistrar.Register<TType, TImpl>(lifeStyle);
            return true;
        }


        /// <summary>
        /// �����ǰû��ע������ͣ�������ʵ����ע������͡�
        /// </summary>
        /// <param name="iocRegistrar">ע����</param>
        /// <param name="lifeStyle">�����������ڵ�����</param>
        /// <returns>���Ϊ������ʵ��ע�ᣬ��Ϊtrue��</returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, impl, lifeStyle);
            return true;
        }

        #endregion
    }
}