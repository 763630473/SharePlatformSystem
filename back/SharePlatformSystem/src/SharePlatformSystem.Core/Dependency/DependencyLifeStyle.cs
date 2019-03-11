namespace SharePlatformSystem.Dependency
{
    /// <summary>
    /// 依赖注入系统中使用的类型的生活方式。
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// 单例对象。在第一次解析时创建了单个对象
        ///同一实例用于后续的解析。
        /// </summary>
        Singleton,

        /// <summary>
        /// 瞬变物体。为每次解析创建一个对象。
        /// </summary>
        Transient
    }
}