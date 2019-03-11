using System;

namespace SharePlatform.Utils.Etc
{
    /// <summary>
    /// ��������ģ�ⲻִ���κβ�����һ�����ļ���
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