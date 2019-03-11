
namespace SharePlatformSystem.Configuration.Startup
{
    /// <summary>
    /// 用于配置“IEventBus”。
    /// </summary>
    public interface IEventBusConfiguration
    {
        /// <summary>
        /// 如果为true，则使用“eventbus.default”。
        ///false，创建per<see cref=“iiocmanager”/>。
        ///通常设置为true。但是，对于单元测试，
        ///可以设置为false。
        ///默认值：真。
        /// </summary>
        bool UseDefaultEventBus { get; set; }
    }
}