using System;

namespace SharePlatform.Utils.Etc
{
    /// <summary>
    /// 此类用于模拟不执行任何操作的一次性文件。
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        public static NullDisposable Instance { get; } = new NullDisposable();

        private NullDisposable()
        {
            
        }

        public void Dispose()
        {

        }
    }
}