using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Realtime
{
    public static class OnlineClientManagerExtensions
    {
        public static bool IsOnline(
            this IOnlineClientManager onlineClientManager,
            long userId)
        {
            return onlineClientManager.GetAllByUserId(userId).Any();
        }

        public static bool Remove(
             this IOnlineClientManager onlineClientManager,
             IOnlineClient client)
        {
            return onlineClientManager.Remove(client.ConnectionId);
        }
    }
}
