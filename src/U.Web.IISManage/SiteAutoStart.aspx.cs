using System;
using System.Diagnostics;
using U.Utilities.IIS;
using U.Utilities.Web;
using U.Utilities;
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
            var isForce = WebHelper.GetInt("isForce", 0);
            if (isForce == 0)
            {
                if (siteName.IsNotNullOrEmpty())
                {
                    IIISUtilService iisService = IISServiceFactory.GetUtilService();
                    iisService.SiteServerRestart(siteName);
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("FAILED【siteName】");
                }
            }
            else {
                if (siteName.IsNotNullOrEmpty())
                {

                    var process = Process.GetProcessesByName("w3wp");
                    foreach (Process p in process)
                    {
                        var username = U.Utilities.CommonHelper.GetProcessUsername(p.Id);
                        if (username.IsNotNullOrEmpty() && username.ToLower() == siteName.ToLower())
                        {
                            p.Kill();
                            break;
                        }
                    }

                    Response.Write("SUCCESS");
                }
                else {
                    Response.Write("FAILED【siteName】");
                }
            }
        }
    }
}