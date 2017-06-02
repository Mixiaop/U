
namespace U.SDK.WeixinShare
{
    /// <summary>
    /// 微信访问票据持久化类，7200秒会过期，用于计划任务（6000秒）的定时刷新
    /// </summary>
    public interface IWeixinTicket
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        string AppId { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        string AppSecret { get; set; }

        /// <summary>
        /// 访问授权
        /// </summary>
        string AccessToken { get; set; }

        /// <summary>
        /// jsapi_ticket是公众号用于调用微信JS接口的临时票据。
        /// 正常情况下，jsapi_ticket的有效期为7200秒，通过access_token来获取。
        /// </summary>
        string JsApiTicket { get; set; }
    }
}
