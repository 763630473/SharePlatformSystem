using System;

namespace SharePlatformSystem.Core.Exceptions
{
    /// <summary>
    ///<see cref=��eventHandler��/>����չ������
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// ʹ�ø����Ĳ�����ȫ�������������¼���
        /// </summary>
        /// <param name="eventHandler">�¼��������</param>
        /// <param name="sender">�¼�����Դ</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// ʹ�ø����Ĳ�����ȫ�������������¼���
        /// </summary>
        /// <param name="eventHandler">�¼��������</param>
        /// <param name="sender">�¼�����Դ</param>
        /// <param name="e">�¼��Ա���</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }

        /// <summary>
        /// ʹ�ø����Ĳ�����ȫ�������������¼���
        /// </summary>
        /// <typeparam name="TEventArgs"><see cref=��eventargs��/>������</typeparam>
        /// <param name="eventHandler">�¼��������</param>
        /// <param name="sender">�¼�����Դ</param>
        /// <param name="e">�¼��Ա���</param>
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