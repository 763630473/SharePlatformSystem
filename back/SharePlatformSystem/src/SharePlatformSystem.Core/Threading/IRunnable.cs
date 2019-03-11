namespace SharePlatformSystem.Threading
{
    /// <summary>
    /// 用于启动/停止自线程服务的接口。
    /// </summary>
    public interface IRunnable
    {
        /// <summary>
        ///启动服务。
        /// </summary>
        void Start();

        /// <summary>
        ///向服务发送stop命令。
        ///服务可能立即返回并异步停止。
        ///然后客户端应调用<see cref=“waitToStop”/>方法以确保它已停止。
        /// </summary>
        void Stop();

        /// <summary>
        /// 等待服务停止。
        /// </summary>
        void WaitToStop();
    }
}
