using System;
using System.Threading;
using JetBrains.Annotations;

namespace SharePlatformSystem
{
    /// <summary>
    ///此类可用于在
    ///调用了dipose方法。
    /// </summary>
    public class DisposeAction : IDisposable
    {
        public static readonly DisposeAction Empty = new DisposeAction(null);

        private Action _action;

        /// <summary>
        ///创建新的“DisposeAction”对象。
        /// </summary>
        /// <param name="action">释放此对象时要执行的操作。</param>
        public DisposeAction([CanBeNull] Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            //联锁防止多次执行动作。
            var action = Interlocked.Exchange(ref _action, null);
            action?.Invoke();
        }
    }
}
