using System;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    ///这些信息是为“AuditedAttribute”方法收集的。
    /// </summary>
    public class AuditInfo
    {
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 模拟用户ID
        /// </summary>
        public string ImpersonatorUserId { get; set; }

        /// <summary>
        /// 服务（类/接口）名称。
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 执行的方法名。
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 调用参数。
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 方法执行的开始时间。
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 方法调用的总持续时间。
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端的IP地址。
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端的名称（通常是计算机名称）。
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        ///如果在Web请求中调用此方法，则提供浏览器信息。
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 可填充和使用的可选自定义数据。
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// 异常对象，如果在方法执行期间发生异常。
        /// </summary>
        public Exception Exception { get; set; }

        public override string ToString()
        {
            var loggedUserId = !string.IsNullOrWhiteSpace(UserId)
                                   ? "user " + UserId
                                   : "an anonymous user";

            var exceptionOrSuccessMessage = Exception != null
                ? "exception: " + Exception.Message
                : "succeed";

            return $"AUDIT LOG: {ServiceName}.{MethodName} is executed by {loggedUserId} in {ExecutionDuration} ms from {ClientIpAddress} IP address with {exceptionOrSuccessMessage}.";
        }
    }
}