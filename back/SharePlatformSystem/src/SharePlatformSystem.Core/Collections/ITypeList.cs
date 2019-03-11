using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Collections
{
    /// <summary>
    ///"ITypeList{TBaseType}"ʹ�ö�����Ϊ�����͵Ŀ�ݷ�ʽ��
    /// </summary>
    public interface ITypeList : ITypeList<object>
    {

    }

    /// <summary>
    /// ��չ"IList{Type}"����Ӷ��ض������͵����ơ�
    /// </summary>
    /// <typeparam name="TBaseType">���б��С�Type���Ļ�����</typeparam>
    public interface ITypeList<in TBaseType> : IList<Type>
    {
        /// <summary>
        /// ���б���������͡�
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// ����б����Ƿ�������͡�
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// ���б���ɾ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Remove<T>() where T : TBaseType;
    }
}