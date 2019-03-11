namespace SharePlatformSystem.Events.Bus
{
    /// <summary>
    ///此接口必须由以下事件数据类实现：
    ///只有一个泛型参数，继承将使用此参数。
    //
    ///例如；
    ///假设学生继承人。当触发EntityCreatedEventData学生，
    ///如果EntityCreatedEventData实现，也会触发EntityCreatedEventData Person
    ///此接口。
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        ///获取自创建此类的新实例以来创建此类的参数。
        /// </summary>
        /// <returns>构造函数参数</returns>
        object[] GetConstructorArgs();
    }
}