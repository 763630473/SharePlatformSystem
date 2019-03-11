using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 此接口用于包装从IOC容器解析的对象。
    ///继承了“idisposable”，解析后的对象很容易释放。
    ///在“IDisposable.Dispose”方法中，调用“IIocResolver.Release”来释放对象。
    ///这是“IDisposableDependencyObjectWrapper t”接口的非通用版本。
    /// </summary>
    public interface IDisposableDependencyObjectWrapper : IDisposableDependencyObjectWrapper<object>
    {

    }
}