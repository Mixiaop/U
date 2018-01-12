using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Realtime
{
    /// <summary>
    /// 表示已连接到应用的在线客户端
    /// </summary>
    public interface IOnlineClient
    {
        /// <summary>
        /// 唯一的连接标识Id
        /// </summary>
        string ConnectionId { get; set; }

        /// <summary>
        /// 客户端的Ip地址
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        long? UserId { get; set; }

        /// <summary>
        /// 此客户端连接建立的时间
        /// </summary>
        DateTime ConnectTime { get; set; }

        /// <summary>
        /// Properties的属性索引
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// 客户端的自定义属性
        /// </summary>
        Dictionary<string, object> Properties { get; }
    }
}
