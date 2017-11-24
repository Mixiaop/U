using System;
using U.Utilities.IIS;
using U.Utilities.Web;

namespace U.Web.IISManage
{
    /// <summary>
    /// 在当前IIS重启指定的站点
    /// </summary>
    public partial class SiteAutoStart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var siteName = WebHelper.GetString("siteName");
            if (siteName.IsNotNullOrEmpty())
            {
                IIISUtilService iisService = IISServiceFactory.GetUtilService();
                iisService.SiteServerRestart(siteName);
                Response.Write("SUCCESS");
            }
            else {
                Response.Write("FAILED【siteName】");
            }
        }
    }
}