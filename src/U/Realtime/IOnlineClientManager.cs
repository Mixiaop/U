using System;
using System.Collections.Generic;

namespace U.Realtime
{
    /// <summary>
    /// 用于管理已连接到应用的在线客户端
    /// </summary>
    public interface IOnlineClientManager
    {
        event EventHandler<OnlineClientEventArgs> ClientConnected;

        event EventHandler<OnlineClientEventArgs> ClientDisconnected;

        event EventHandler<OnlineUserEventArgs> UserConnected;

        event EventHandler<OnlineUserEventArgs> UserDisconnected;

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client"></param>
        void Add(IOnlineClient client);

        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="connectionId"></param>
        bool Remove(string connectionId);

        /// <summary>
        /// 通过连接标识Id找到在线客户端
        /// 未找到则返回null
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        IOnlineClient GetByConnectionIdOrNull(string connectionId);

        /// <summary>
        /// 获取所有在线客户端
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<IOnlineClient> GetAllClients();

        IReadOnlyList<IOnlineClient> GetAllByUserId(long userId);
    }
}
