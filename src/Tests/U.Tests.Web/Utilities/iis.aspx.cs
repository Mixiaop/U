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
            btnRemoveDomain.Click += btnRemoveDomain_Click;
            btnRestart.Click += BtnRestart_Click;
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

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            IIS7UtilService iisService = (IIS7UtilService)IISServiceFactory.GetUtilService();
            //Response.Write(iisService.SiteServerRestart("U.Tests"));

            var server = new ServerManager();
            Site site = null;
            //找指定站点
            foreach (Site s in server.Sites)
            {
                if (s.Name.EqualsEx("U.Tests"))
                {
                    site = s;
                    break;
                }
            }
            if (site != null)
            {
                //site.ServerAutoStart = true;
                site.Stop();
                System.Threading.Thread.Sleep(3000);
                site.Start();
                //site.Start();
                //server.CommitChanges();
                Response.Write("success");
            }
            else{
                Response.Write("failed");
            }
        }

        void btnRemoveDomain_Click(object sender, EventArgs e)
        {
            ServerManager server = new ServerManager();
            IIS7UtilService iisService = (IIS7UtilService)IISServiceFactory.GetUtilService();
            iisService.RemoveSiteDomain("U.Tests", "aa.youzy.cn");
            //Site site = server.Sites["U.Tests"];
            //for (int i = 0; i < site.Bindings.Count; i++)
            //{
            //    var bind = site.Bindings[i];
            //    if (bind.Host.EqualsEx("test2.youzy.cn") && bind.BindingInformation.Contains("80"))
            //    {
            //        Response.Write("remove " + bind.BindingInformation);
            //        site.Bindings.RemoveAt(i);
            //        break;
            //    }
            //}
            //site.ServerAutoStart = true;
            //server.CommitChanges();
            Response.Write("remove sus");
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