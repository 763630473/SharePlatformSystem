using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using SharePlatformSystem.Core;
using SharePlatformSystem.Core.Collections.Extensions;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Dependency;

namespace SharePlatformSystem.RealTime
{
    public class OnlineClientManager<T> : OnlineClientManager, IOnlineClientManager<T>
    {

    }

    /// <summary>
    /// 实现 <see cref="IOnlineClientManager"/>.
    /// </summary>
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        public event EventHandler<OnlineClientEventArgs> ClientConnected;
        public event EventHandler<OnlineClientEventArgs> ClientDisconnected;
        public event EventHandler<OnlineUserEventArgs> UserConnected;
        public event EventHandler<OnlineUserEventArgs> UserDisconnected;

        /// <summary>
        /// 在线客户端
        /// </summary>
        protected ConcurrentDictionary<string, IOnlineClient> Clients { get; }

        protected readonly object SyncObj = new object();

        /// <summary>
        /// 初始化的新实例<see cref="OnlineClientManager"/> .
        /// </summary>
        public OnlineClientManager()
        {
            Clients = new ConcurrentDictionary<string, IOnlineClient>();
        }

        public virtual void Add(IOnlineClient client)
        {
            lock (SyncObj)
            {
                var userWasAlreadyOnline = false;
                var user = client.ToUserIdentifierOrNull();

                if (user != null)
                {
                    userWasAlreadyOnline = this.IsOnline(user);
                }

                Clients[client.ConnectionId] = client;

                ClientConnected.InvokeSafely(this, new OnlineClientEventArgs(client));

                if (user != null && !userWasAlreadyOnline)
                {
                    UserConnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                }
            }
        }

        public virtual bool Remove(string connectionId)
        {
            lock (SyncObj)
            {
                var isRemoved = Clients.TryRemove(connectionId, out IOnlineClient client);

                if (isRemoved)
                {
                    var user = client.ToUserIdentifierOrNull();

                    if (user != null && !this.IsOnline(user))
                    {
                        UserDisconnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                    }

                    ClientDisconnected.InvokeSafely(this, new OnlineClientEventArgs(client));
                }

                return isRemoved;
            }
        }

        public virtual IOnlineClient GetByConnectionIdOrNull(string connectionId)
        {
            lock (SyncObj)
            {
                return Clients.GetOrDefault(connectionId);
            }
        }
        
        public virtual IReadOnlyList<IOnlineClient> GetAllClients()
        {
            lock (SyncObj)
            {
                return Clients.Values.ToImmutableList();
            }
        }

        [NotNull]
        public virtual IReadOnlyList<IOnlineClient> GetAllByUserId([NotNull] IUserIdentifier user)
        {
            Check.NotNull(user, nameof(user));

            return GetAllClients()
                 .Where(c =>c.UserId == user.UserId)
                 .ToImmutableList();
        }
    }
}