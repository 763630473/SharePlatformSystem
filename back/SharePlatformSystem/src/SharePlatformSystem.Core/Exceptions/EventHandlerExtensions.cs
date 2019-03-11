using System;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    ///<see cref=“eventHandler”/>的扩展方法。
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// 使用给定的参数安全地引发给定的事件。
        /// </summary>
        /// <param name="eventHandler">事件处理程序</param>
        /// <param name="sender">事件的来源</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// 使用给定的参数安全地引发给定的事件。
        /// </summary>
        /// <param name="eventHandler">事件处理程序</param>
        /// <param name="sender">事件的来源</param>
        /// <param name="e">事件自变量</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }

        /// <summary>
        /// 使用给定的参数安全地引发给定的事件。
        /// </summary>
        /// <typeparam name="TEventArgs"><see cref=“eventargs”/>的类型</typeparam>
        /// <param name="eventHandler">事件处理程序</param>
        /// <param name="sender">事件的来源</param>
        /// <param name="e">事件自变量</param>
        public static void InvokeSafely<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }
    }
}