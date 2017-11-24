using System;
using U;
using U.Utilities.IIS;
using U.Utilities.Web;

namespace U.Web.IISManage.Domains
{
    /// <summary>
    /// 在当前IIS下为指定的站点添加域名
    /// </summary>
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string siteName = WebHelper.GetString("siteName");
            string domainUrl = WebHelper.GetString("domainUrl");
            

            if (domainUrl.IsNotNullOrEmpty() && siteName.IsNotNullOrEmpty())
            {
                domainUrl = domainUrl.Replace("http://", "").Replace("https://", "").TrimEnd("/").Trim();

                IIISUtilService iisService = IISServiceFactory.GetUtilService();
                iisService.AddSiteDomain(siteName, domainUrl);
                Response.Write("SUCCESS");
            }
            else
            {
                Response.Write("FAILED【siteName、domainUrl】");
            }
        }
    }
}