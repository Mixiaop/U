using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using U.Utilities.Net;

namespace U.Tests.Web.Utilities
{
    public partial class WebSiteTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            var pageBytes = client.DownloadData("http://www.youzy.cn");
            var pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);
            Response.Write(pageHtml);
        }
    }
}