using System;
using SharePlatformSystem.Events.Bus.Handlers;

namespace SharePlatformSystem.Events.Bus.Factories
{
    /// <summary>
    /// 为负责创建/获取和发布事件处理程序的工厂定义接口。
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        ///获取事件处理程序。
        /// </summary>
        /// <returns>事件处理程序</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// 获取处理程序的类型（不创建实例）。
        /// </summary>
        /// <returns></returns>
        Type GetHandlerType();

        /// <summary>
        /// 释放事件处理程序。
        /// </summary>
        /// <param name="handler">释放把手</param>
        void ReleaseHandler(IEventHandler handler);
    }
}