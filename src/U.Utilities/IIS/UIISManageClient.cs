using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using U.Utilities.Net;
using U.Application.Services.Dto;

namespace U.Utilities.IIS
{
    /// <summary>
    /// U.Web.IISManage站点的客户端调用
    /// </summary>
    public class UIISManageClient
    {
        /// <summary>
        /// 重启指定站点
        /// </summary>
        /// <param name="host"></param>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static StateOutput SiteAutoStart(string host, string siteName) {
            StateOutput res = new StateOutput();
            if (siteName.IsNotNullOrEmpty())
            {
                if (!host.EndsWith("/"))
                    host += "/";

                if (!host.Contains("http://"))
                    host = "http://" + host;

                var soaUrl = host + "SiteAutoStart.aspx";
                soaUrl += "?siteName=" + siteName;

                var message = WebRequestHelper.HttpGet(soaUrl, Encoding.GetEncoding("utf-8"));
                if (message.ToLower() != "success")
                {
                    res.AddError(message);
                }
            }
            else
            {
                res.AddError("siteName is null");
            }

            return res;
        }

        public static StateOutput<List<IISDomain>> SiteGetDomains(string host, string siteName)
        {
            StateOutput<List<IISDomain>> res = new StateOutput<List<IISDomain>>();
            if (siteName.IsNotNullOrEmpty())
            {
                if (!host.EndsWith("/"))
                    host += "/";

                if (!host.Contains("http://"))
                    host = "http://" + host;

                var soaUrl = host + "Domains/List.aspx";
                soaUrl += "?siteName=" + siteName;

                var message = WebRequestHelper.HttpGet(soaUrl, Encoding.GetEncoding("utf-8"));
                if (message.ToLower().Contains("failed"))
                {
                    res.AddError(message);
                }
                else {
                    res.Items = JsonConvert.DeserializeObject<List<IISDomain>>(message);
                }
            }
            else
            {
                res.AddError("siteName is null");
            }

            return res;
        }

        /// <summary>
        /// 为指定站点添加域名
        /// </summary>
        /// <param name="host">主机地址</param>
        /// <param name="siteName"></param>
        /// <param name="domainUrl"></param>
        /// <returns></returns>
        public static StateOutput SiteAddDomain(string host, string siteName, string domainUrl) {
            StateOutput res = new StateOutput();
            if (siteName.IsNotNullOrEmpty() && domainUrl.IsNotNullOrEmpty())
            {
                if (!host.EndsWith("/"))
                    host += "/";

                if (!host.Contains("http://"))
                    host = "http://" + host;

                var soaUrl = host + "Domains/Add.aspx";
                soaUrl += "?siteName=" + siteName;
                soaUrl += "&domainUrl=" + domainUrl;

                var message = WebRequestHelper.HttpGet(soaUrl, Encoding.GetEncoding("utf-8"));
                if (message.ToLower() != "success") {
                    res.AddError(message);
                }
            }
            else {
                res.AddError("siteName or domainUrl is null");
            }

            return res;
        }

        /// <summary>
        /// 为指定站点添加域名
        /// </summary>
        /// <param name="host">主机地址</param>
        /// <param name="siteName"></param>
        /// <param name="domainUrl"></param>
        /// <returns></returns>
        public static StateOutput SiteRemoveDomain(string host, string siteName, string domainUrl)
        {
            StateOutput res = new StateOutput();
            if (siteName.IsNotNullOrEmpty() && domainUrl.IsNotNullOrEmpty())
            {
                if (!host.EndsWith("/"))
                    host += "/";

                if (!host.Contains("http://"))
                    host = "http://" + host;

                var soaUrl = host + "Domains/Remove.aspx";
                soaUrl += "?siteName=" + siteName;
                soaUrl += "&domainUrl=" + domainUrl;

                var message = WebRequestHelper.HttpGet(soaUrl, Encoding.GetEncoding("utf-8"));
                if (message.ToLower() != "success")
                {
                    res.AddError(message);
                }
            }
            else
            {
                res.AddError("siteName or domainUrl is null");
            }

            return res;
        }
    }
}
