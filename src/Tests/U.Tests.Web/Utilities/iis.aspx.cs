using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.Administration;
using Newtonsoft.Json;
using U.Utilities.IIS;
namespace U.Tests.Web.Utilities
{
    public partial class iis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDomainList.Click += BtnDomainList_Click;
            btnAddDomain.Click += BtnAddDomain_Click;
            //Response.Write(IISServiceFactory.GetIISMajorVersion());

            //IIISUtilService iisService = IISServiceFactory.GetUtilService();

            //ServerManager server = new ServerManager();
            //foreach (Site site in server.Sites) {
            //    //Response.Write(site.Name + "<br />");
            //    if (site.Name == "U.Tests")
            //    {
            //        foreach (var bind in site.Bindings)
            //        {
            //            Response.Write(bind.BindingInformation + "<br />");
            //            Response.Write(bind.Host + "<br />");
            //        }
            //    }
            //        //    //site.Bindings.Add(string.Format("*:80:{0}", "mbjuan.com"), "http");
            //        //    //server.CommitChanges();
            //        //    //Response.Write("成功了");
            //        //    //site.ServerAutoStart();
            //        //}
            //    }



        }

        private void BtnAddDomain_Click(object sender, EventArgs e)
        {
            IIS7UtilService iisService = (IIS7UtilService)IISServiceFactory.GetUtilService();

            iisService.AddSiteDomain("U.Tests", "aa.youzy.cn", 80);
            //var list = iisService.GetSiteDomains("U.Tests");

            //Response.Write(JsonConvert.SerializeObject(list));
            //Response.Write(iisService.GetSite("U.Tests"));
        }

        private void BtnDomainList_Click(object sender, EventArgs e)
        {
            IIISUtilService iisService = IISServiceFactory.GetUtilService();
            var list = iisService.GetSiteDomains("U.Tests");

            Response.Write(JsonConvert.SerializeObject(list));
        }
    }
}