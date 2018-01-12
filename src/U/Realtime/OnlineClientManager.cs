using System;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Realtime
{
    public class OnlineClientManager : IOnlineClientManager
    {
        public event EventHandler<OnlineClientEventArgs> ClientConnected;
        public event EventHandler<OnlineClientEventArgs> ClientDisconnected;
        public event EventHandler<OnlineUserEventArgs> UserConnected;
        public event EventHandler<OnlineUserEventArgs> UserDisconnected;

        protected ConcurrentDictionary<string, IOnlineClient> Clients { get; }

        protected readonly object SyncObj = new object();

        public OnlineClientManager()
        {
            Clients = new ConcurrentDictionary<string, IOnlineClient>();
        }

        public virtual void Add(IOnlineClient client)
        {
            lock (SyncObj)
            {
                var userWasAlreadyOnline = false;

                if (client.UserId.HasValue)
                {
                    userWasAlreadyOnline = this.IsOnline(client.UserId.Value);
                }

                Clients[client.ConnectionId] = client;

                ClientConnected.InvokeSafely(this, new OnlineClientEventArgs(client));

                if (client.UserId.HasValue && !userWasAlreadyOnline)
                {
                    UserConnected.InvokeSafely(this, new OnlineUserEventArgs(client.UserId.Value, client));
                }
            }
        }

        public virtual bool Remove(string connectionId)
        {
            lock (SyncObj)
            {
                IOnlineClient client;
                var isRemoved = Clients.TryRemove(connectionId, out client);

                if (isRemoved)
                {
                    if ( client.UserId.HasValue && !this.IsOnline(client.UserId.Value))
                    {
                        UserDisconnected.InvokeSafely(this, new OnlineUserEventArgs(client.UserId.Value, client));
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


        public virtual IReadOnlyList<IOnlineClient> GetAllByUserId(long userId)
        {
            return GetAllClients()
                 .Where(c => (c.UserId == userId))
                 .ToImmutableList();
        }
    }
}
