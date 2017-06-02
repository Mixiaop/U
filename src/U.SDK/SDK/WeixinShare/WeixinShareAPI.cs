using U.SDK.Weixin;

namespace U.SDK.WeixinShare
{
    /// <summary>
    /// 微信分享API
    /// </summary>
    public class WeixinShareAPI
    {
        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateNonce()
        {
            return Helper.CreateNonce_str();
        }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        /// <returns></returns>
        public static long CreateTimestamp()
        {
            return Helper.CreateTimestamp();
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appid">app id</param>
        /// <param name="secrect">app secrect</param>
        /// <returns></returns>
        public static string GetAccessToken(string appid, string secrect)
        {
            return BasicAPI.GetAccessToken(appid, secrect).access_token;
        }

        /// <summary>
        /// 获取jsapi_ticket
        /// jsapi_ticket是公众号用于调用微信JS接口的临时票据。
        /// 正常情况下，jsapi_ticket的有效期为7200秒，通过access_token来获取。
        /// 由于获取jsapi_ticket的api调用次数非常有限，频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，开发者必须在自己的服务全局缓存jsapi_ticket 。
        /// </summary>
        /// <param name="access_token">BasicAPI获取的access_token,也可以通过TokenHelper获取</param>
        /// <returns></returns>
        public static string GetTickect(string access_token)
        {
            return WxJsSdk.GetTickect(access_token).ticket;
        }

        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <param name="noncestr">随机字符串(必须与wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(必须与wx.config中的timestamp相同)</param>
        /// <param name="metaStr">签名源字符串</param>
        /// <returns></returns>
        public static string GetSignature(string jsapi_ticket, string noncestr, long timestamp, string url, out string metaStr)
        {
            return WxJsSdk.GetSignature(jsapi_ticket, noncestr, timestamp, url, out metaStr);
        }
    }
}
