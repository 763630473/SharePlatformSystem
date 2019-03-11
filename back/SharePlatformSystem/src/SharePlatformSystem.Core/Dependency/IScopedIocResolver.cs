using System;

namespace SharePlatformSystem.Dependency
{
    /// <summary>
    ///此接口用于将批处理解决的作用域包装在一个<c>using->c>语句中。
    ///继承了“idisposable”和“iiocresolver”，解析后的对象很容易批量
    ///iocdolver释放的方式。
    ///在“IDisposable.Dispose”方法中，调用“IIocResolver.Release”来释放对象。
    /// </summary>
    public interface IScopedIocResolver : IIocResolver, IDisposable { }
}