using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// �˽ӿ����ڰ�װ��IOC���������Ķ���
    ///�̳��ˡ�idisposable����������Ķ���������ͷš�
    ///�ڡ�IDisposable.Dispose�������У����á�IIocResolver.Release�����ͷŶ���
    ///���ǡ�IDisposableDependencyObjectWrapper t���ӿڵķ�ͨ�ð汾��
    /// </summary>
    public interface IDisposableDependencyObjectWrapper : IDisposableDependencyObjectWrapper<object>
    {

    }
}