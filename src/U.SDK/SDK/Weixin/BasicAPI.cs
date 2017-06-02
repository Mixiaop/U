﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;

namespace U.SDK.Weixin
{
    /// <summary>
    /// 对应微信API的 "基础支持"
    /// </summary>
    public class BasicAPI
    {
        /// <summary>
        /// 检查签名是否正确:
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token">AccessToken</param>
        /// <returns>
        /// true: check signature success
        /// false: check failed, 非微信官方调用!
        /// </returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string token, out string ent)
        {
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            ent = enText.ToString();
            return signature == enText.ToString();
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="grant_type"></param>
        /// <param name="appid"></param>
        /// <param name="secrect"></param>
        /// <returns>access_toke</returns>
        public static dynamic GetAccessToken(string appid, string secrect)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}", "client_credential", appid, secrect);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            var token = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
            return token;
        }

        /// <summary>
        /// 上传多媒体文件
        /// 1.上传的媒体文件限制：
        ///图片（image) : 1MB，支持JPG格式
        ///语音（voice）：1MB，播放长度不超过60s，支持MP4格式
        ///视频（video）：10MB，支持MP4格式
        ///缩略图（thumb)：64KB，支持JPG格式
        ///2.媒体文件在后台保存时间为3天，即3天后media_id失效
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）</param>
        /// <param name="file">本地完整路径</param>
        /// <returns>media_id</returns>
        public static string UploadMedia(string access_token, string type, string file)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", access_token, type.ToString());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            var returnText = Helper.HttpRequestPost(url, fileDictionary);
            if (returnText.Contains("errcode"))
            {
                return string.Empty;
            }
            var media = DynamicJson.Parse(returnText);
            return media.media_id;
        }

        /// <summary>
        /// 下载多媒体
        /// 视频文件不支持下载，调用该接口需http协议。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="media_id"></param>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public static void DownloadMedia(string access_token, string media_id, Stream sm)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", access_token, media_id);
            Helper.Download(url, sm);
        }
    }
}
