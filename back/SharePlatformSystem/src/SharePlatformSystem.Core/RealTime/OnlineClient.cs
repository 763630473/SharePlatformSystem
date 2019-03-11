using System;
using System.Collections.Generic;
using SharePlatformSystem.Core.Timing;
using SharePlatformSystem.Json;

namespace SharePlatformSystem.RealTime
{
    /// <summary>
    /// 实现 <see cref="IOnlineClient"/>.
    /// </summary>
    [Serializable]
    public class OnlineClient : IOnlineClient
    {
        /// <summary>
        /// 此客户端的唯一连接ID。
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 此客户端的IP地址。
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 此客户端的连接建立时间。
        /// </summary>
        public DateTime ConnectTime { get; set; }

        /// <summary>
        /// 设置/获取的快捷方式 <see cref="Properties"/>.
        /// </summary>
        public object this[string key]
        {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        /// <summary>
        /// 可用于添加此客户端的自定义属性。
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _properties = value;
            }
        }
        private Dictionary<string, object> _properties;

        /// <summary>
        ///初始化类的新实例<see cref="OnlineClient"/>.
        /// </summary>
        public OnlineClient()
        {
            ConnectTime = Clock.Now;
        }

        /// <summary>
        ///初始化类的新实例<see cref="OnlineClient"/> .
        /// </summary>
        /// <param name="connectionId">连接标识符。</param>
        /// <param name="ipAddress">IP地址。</param>
        /// <param name="userId">用户标识符。</param>
        public OnlineClient(string connectionId, string ipAddress, string userId)
            : this()
        {
            ConnectionId = connectionId;
            IpAddress = ipAddress;
            UserId = userId;

            Properties = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}