using System;
using U.Utilities.IIS;
using U.Utilities.Web;

namespace U.Web.IISManage.Domains
{
    /// <summary>
    /// 在当前IIS下移除指定站点下的域名
    /// </summary>
    public partial class Remove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string siteName = WebHelper.GetString("siteName");
            string domainUrl = WebHelper.GetString("domainUrl");
            int port = WebHelper.GetInt("port", 80);

            if (siteName.IsNotNullOrEmpty() && domainUrl.IsNotNullOrEmpty())
            {
                domainUrl = domainUrl.Replace("http://", "").Replace("https://", "").TrimEnd("/").Trim();

                IIISUtilService iisService = IISServiceFactory.GetUtilService();
                iisService.RemoveSiteDomain(siteName, domainUrl, port);
                Response.Write("SUCCESS");
            }
            else
            {
                Response.Write("FAILED【siteName、domainUrl】");
            }
        }
    }
}