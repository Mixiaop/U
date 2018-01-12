using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using U.Logging;
using U.Utilities.IO;

namespace U.Utilities.Net
{
    /// <summary>
    /// 获取请求结果的助手类
    /// 发送GET或POST请求，一般用于调用远程接口
    /// </summary>
    public static class WebRequestHelper
    {

        #region Proxy
        private static WebProxy _webProxy = null;

        /// <summary>
        /// 设置Web代理
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void SetHttpProxy(string host, string port, string username, string password)
        {
            ICredentials cred = new NetworkCredential(username, password);
            if (host.IsNotNullOrEmpty())
            {
                _webProxy = new WebProxy(string.Format("{0}:{1}", host, port ?? "80"), true, null, cred);
            }
        }

        /// <summary>
        /// 移除Web代理
        /// </summary>
        public static void RemoveHttpProxy()
        {
            _webProxy = null;
        }
        #endregion

        #region Sync GET / POST
        /// <summary>
        /// 使用GET方式获取结果（无COOKIE）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding encoding = null)
        {
            WebClient client = new WebClient();
            client.Proxy = _webProxy;
            client.Encoding = encoding ?? Encoding.UTF8;
            return client.DownloadString(url);
        }

        /// <summary>
        /// 使用GET方式获取结果（COOKIE及证书）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers">自定义Headers参数</param>
        /// <param name="encoding"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="cer">证书，不需要则保留NULL</param>
        /// <param name="timeout">默认 10 秒</param>
        /// <returns></returns>
        public static string HttpGet(string url, Dictionary<string, string> headers = null, Encoding encoding = null, CookieContainer cookieContainer = null, X509Certificate cer = null, int timeout = 10000)
        {
            encoding = encoding ?? Encoding.GetEncoding("utf-8");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = timeout;
            request.Proxy = _webProxy;

            if (headers != null)
            {

                foreach (var head in headers)
                {
                    request.Headers.Add(head.Key, head.Value);
                }
            }

            if (cer != null)
            {
                request.ClientCertificates.Add(cer);
            }

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                res.Cookies = cookieContainer.GetCookies(res.ResponseUri);
            }

            using (Stream resStream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(resStream, encoding))
                {
                    string resString = reader.ReadToEnd();
                    return resString;
                }
            }

        }

        /// <summary>
        /// 使用POST方式获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="fileDict"></param>
        /// <param name="refererUrl"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="cer"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> formData = null, Dictionary<string, string> fileDict = null, string refererUrl = null, Dictionary<string, string> headers = null,
            Encoding encoding = null, CookieContainer cookieContainer = null, X509Certificate cer = null, int timeout = 10000) {
                MemoryStream ms = new MemoryStream();
                formData.FillFormDataStream(ms);//填充formData
                return HttpPost(url, ms, fileDict, refererUrl, headers, encoding, cookieContainer, cer, timeout);
        }

        public static string HttpPost(string url, string formData = null, Dictionary<string, string> fileDict = null, string refererUrl = null, Dictionary<string, string> headers = null,
            Encoding encoding = null, CookieContainer cookieContainer = null, X509Certificate cer = null, int timeout = 10000)
        {
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData
            return HttpPost(url, ms, fileDict, refererUrl, headers, encoding, cookieContainer, cer, timeout);
        }

        /// <summary>
        /// 使用POST方式获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream">表单参数转Stream</param>
        /// <param name="fileDict">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="refererUrl"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="cet">证书，如果不需要则保留null</param>
        /// <param name="timeout">默认 10 秒</param>
        /// <returns></returns>
        public static string HttpPost(string url, Stream postStream = null, Dictionary<string, string> fileDict = null, string refererUrl = null, Dictionary<string, string> headers = null,
            Encoding encoding = null, CookieContainer cookieContainer = null, X509Certificate cer = null, int timeout = 10000)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = timeout;
            request.Proxy = _webProxy;
            if (cer != null)
            {
                request.ClientCertificates.Add(cer);
            }

            

            #region 处理Form表单文件上传
            var formUploadFile = fileDict != null && fileDict.Count > 0;//是否用Form上传文件
            if (formUploadFile)
            {
                //通过表单上传文件
                postStream = postStream ?? new MemoryStream();

                string boundary = "----" + DateTime.Now.Ticks.ToString("x");
                //byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                string fileFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                string dataFormdataTemplate = "\r\n--" + boundary +
                                                "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (var file in fileDict)
                {
                    try
                    {
                        var fileName = file.Value;
                        //准备文件流
                        using (var fileStream = FileHelper.GetFileStream(fileName))
                        {
                            string formdata = null;
                            if (fileStream != null)
                            {
                                //存在文件
                                formdata = string.Format(fileFormdataTemplate, file.Key, /*fileName*/ Path.GetFileName(fileName));
                            }
                            else
                            {
                                //不存在文件或只是注释
                                formdata = string.Format(dataFormdataTemplate, file.Key, file.Value);
                            }

                            //统一处理
                            var formdataBytes = Encoding.UTF8.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);

                            //写入文件
                            if (fileStream != null)
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead = 0;
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    postStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //结尾
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;

            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                //debug
                //postStream.Seek(0, SeekOrigin.Begin);
                //StreamReader sr = new StreamReader(postStream);
                //var postStr = sr.ReadToEnd();

                postStream.Close();//关闭文件访问
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        /// <summary>
        /// 填充表单信息到 Stream
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="stream"></param>
        public static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        }

        public static void FillFormDataStream(this string data, Stream stream) {
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        }
        #endregion

        #region Async GET / POST
        /// <summary>
        /// 使用GET方式获取结果（无COOKIE）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, Encoding encoding = null)
        {
            WebClient client = new WebClient();
            client.Proxy = _webProxy;
            client.Encoding = encoding ?? Encoding.UTF8;
            return await client.DownloadStringTaskAsync(url);
        }

        /// <summary>
        /// 使用GET方式获取结果（COOKIE及证书）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers">自定义Headers参数</param>
        /// <param name="encoding"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="cer">证书，不需要则保留NULL</param>
        /// <param name="timeout">默认 10 秒</param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, Dictionary<string, string> headers = null, Encoding encoding = null, CookieContainer cookieContainer = null, X509Certificate cer = null, int timeout = 10000)
        {
            encoding = encoding ?? Encoding.GetEncoding("utf-8");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = timeout;
            request.Proxy = _webProxy;

            if (headers != null)
            {

                foreach (var head in headers)
                {
                    request.Headers.Add(head.Key, head.Value);
                }
            }

            if (cer != null)
            {
                request.ClientCertificates.Add(cer);
            }

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            HttpWebResponse res = (HttpWebResponse)(await request.GetResponseAsync());
            if (cookieContainer != null)
            {
                res.Cookies = cookieContainer.GetCookies(res.ResponseUri);
            }

            using (Stream resStream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(resStream, encoding))
                {
                    string resString = await reader.ReadToEndAsync();
                    return resString;
                }
            }

        }

        /// <summary>
        /// 使用POST方式获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="fileDict"></param>
        /// <param name="refererUrl"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="cer"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, Dictionary<string, string> formData = null, Dictionary<string, string> fileDict = null, string refererUrl = null, Dictionary<string, string> headers = null,
            Encoding encoding = null, CookieContainer cookieContainer = null, X509Certificate cer = null, int timeout = 10000)
        {
            MemoryStream ms = new MemoryStream();
            await formData.FillFormDataStreamAsync(ms);//填充formData
            return await HttpPostAsync(url, ms, fileDict, refererUrl, headers, encoding, cookieContainer, cer, timeout);
        }

        /// <summary>
        /// 使用POST方式获取结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream">表单参数转Stream</param>
        /// <param name="fileDict">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="refererUrl"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="cet">证书，如果不需要则保留null</param>
        /// <param name="timeout">默认 10 秒</param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, Stream postStream = null, Dictionary<string, string> fileDict = null, string refererUrl = null, Dictionary<string, string> headers = null,
            Encoding encoding = null, CookieContainer cookieContainer = null, X509Certificate cer = null, int timeout = 10000)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = timeout;
            request.Proxy = _webProxy;
            if (cer != null)
            {
                request.ClientCertificates.Add(cer);
            }


            #region 处理Form表单文件上传
            var formUploadFile = fileDict != null && fileDict.Count > 0;//是否用Form上传文件
            if (formUploadFile)
            {
                //通过表单上传文件
                postStream = postStream ?? new MemoryStream();

                string boundary = "----" + DateTime.Now.Ticks.ToString("x");
                //byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                string fileFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                string dataFormdataTemplate = "\r\n--" + boundary +
                                                "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (var file in fileDict)
                {
                    try
                    {
                        var fileName = file.Value;
                        //准备文件流
                        using (var fileStream = FileHelper.GetFileStream(fileName))
                        {
                            string formdata = null;
                            if (fileStream != null)
                            {
                                //存在文件
                                formdata = string.Format(fileFormdataTemplate, file.Key, /*fileName*/ Path.GetFileName(fileName));
                            }
                            else
                            {
                                //不存在文件或只是注释
                                formdata = string.Format(dataFormdataTemplate, file.Key, file.Value);
                            }

                            //统一处理
                            var formdataBytes = Encoding.UTF8.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);

                            //写入文件
                            if (fileStream != null)
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead = 0;
                                while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                                {
                                    await postStream.WriteAsync(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //结尾
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                await postStream.WriteAsync(footer, 0, footer.Length);

                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;

            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = await request.GetRequestStreamAsync();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = await postStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    await requestStream.WriteAsync(buffer, 0, bytesRead);
                }

                //debug
                //postStream.Seek(0, SeekOrigin.Begin);
                //StreamReader sr = new StreamReader(postStream);
                //var postStr = sr.ReadToEnd();

                postStream.Close();//关闭文件访问
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());

            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
                {
                    string retString = await myStreamReader.ReadToEndAsync();
                    return retString;
                }
            }
        }


        /// <summary>
        /// 填充表单信息的Stream
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="stream"></param>
        public static async Task FillFormDataStreamAsync(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            await stream.WriteAsync(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        }
        #endregion

        /// <summary>
        /// 从Url下载到流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        public static void Download(string url, Stream stream)
        {
            WebClient wc = new WebClient();
            var data = wc.DownloadData(url);
            foreach (var b in data)
            {
                stream.WriteByte(b);
            }
        }

        /// <summary>
        /// 组装QueryString
        /// 参数之间用&amp;连接，首位没有符号，如：a=1&amp;b=2
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 封装System.Web.HttpUtility.HtmlEncode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string html)
        {
            return System.Web.HttpUtility.HtmlEncode(html);
        }
        /// <summary>
        /// 封装System.Web.HttpUtility.HtmlDecode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string html)
        {
            return System.Web.HttpUtility.HtmlDecode(html);
        }
        /// <summary>
        /// 封装System.Web.HttpUtility.UrlEncode
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(this string url)
        {
            return System.Web.HttpUtility.UrlEncode(url);
        }
        /// <summary>
        /// 封装System.Web.HttpUtility.UrlDecode
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(this string url)
        {
            return System.Web.HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// <para>将 URL 中的参数名称/值编码为合法的格式。</para>
        /// <para>可以解决类似这样的问题：假设参数名为 tvshow, 参数值为 Tom&Jerry，如果不编码，可能得到的网址： http://a.com/?tvshow=Tom&Jerry&year=1965 编码后则为：http://a.com/?tvshow=Tom%26Jerry&year=1965 </para>
        /// <para>实践中经常导致问题的字符有：'&', '?', '=' 等</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string AsUrlData(this string data)
        {
            if (data == null)
            {
                return null;
            }
            return Uri.EscapeDataString(data);
        }
    }
}
