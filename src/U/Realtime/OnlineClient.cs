using System;
using System.Collections.Generic;
using U.Timing;

namespace U.Realtime
{
    [Serializable]
    public class OnlineClient : IOnlineClient
    {

        public OnlineClient()
        {
            ConnectTime = Clock.Now;
        }

        public OnlineClient(string connectionId, string ipAddress, long? userId)
            : this()
        {
            ConnectionId = connectionId;
            IpAddress = ipAddress;
            UserId = userId;

            Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 唯一的连接标识Id
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 客户端的Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 此客户端连接建立的时间
        /// </summary>
        public DateTime ConnectTime { get; set; }

        /// <summary>
        /// Properties的属性索引
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key] {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        private Dictionary<string, object> _properties;
        /// <summary>
        /// 客户端的自定义属性
        /// </summary>
        public Dictionary<string, object> Properties {
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


        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}
