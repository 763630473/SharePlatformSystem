using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    ///�˽ӿ����ڰ�װ��IOC���������Ķ���
    ///�̳��ˡ�idisposable����������Ķ���������ͷš�
    ///�ڡ�IDisposable.Dispose�������У����á�IIocResolver.Release�����ͷŶ���
    /// </summary>
    /// <typeparam name="T">���������/typeparam>
    public interface IDisposableDependencyObjectWrapper<out T> : IDisposable
    {
        /// <summary>
        /// �����Ķ���
        /// </summary>
        T Object { get; }
    }
}