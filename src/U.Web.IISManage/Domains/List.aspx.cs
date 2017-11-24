using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using U.Utilities.IIS;
using U.Utilities.Web;

namespace U.Web.IISManage.Domains
{
    /// <summary>
    /// 获取当前IIS下指定站点的所有域名
    /// </summary>
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var siteName = WebHelper.GetString("siteName");

            if (siteName.IsNotNullOrEmpty())
            {
                IIISUtilService iisService = IISServiceFactory.GetUtilService();
                IList<IISDomain> domains = iisService.GetSiteDomains(siteName);

                Response.Write(JsonConvert.SerializeObject(domains));
            }
            else {
                Response.Write("FAILED【siteName】");
            }
        }
    }
}