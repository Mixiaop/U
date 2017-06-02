using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.Administration;
using U.Utilities.IIS;
namespace U.Tests.Web.Utilities
{
    public partial class iis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(IISServiceFactory.GetIISMajorVersion());

            //IIISUtilService iisService = IISServiceFactory.GetUtilService();

            ServerManager server = new ServerManager();
            foreach (var site in server.Sites) {
                if (site.Name == "mbjuan.soa") {
                    foreach (var bind in site.Bindings) {
                        Response.Write(bind.BindingInformation + "<br />");
                        Response.Write(bind.Host + "<br />");
                    }
                    
                    //site.Bindings.Add(string.Format("*:80:{0}", "mbjuan.com"), "http");
                    //server.CommitChanges();
                    //Response.Write("成功了");
                    //site.ServerAutoStart();
                }
            }

            

        }
    }
}