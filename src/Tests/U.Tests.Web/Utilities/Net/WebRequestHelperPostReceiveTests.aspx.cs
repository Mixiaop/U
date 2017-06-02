using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Web;

namespace U.Tests.Web.Utilities.Net
{
    public partial class WebRequestHelperPostReceiveTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var a = Request.Form;
            var b = Request.QueryString;
            var postKey = WebHelper.GetFormString("key");// Request.Form["key"];
            var getKey = WebHelper.GetString("key"); // Request.QueryString["key"];
            Response.Write("postKey：" + postKey + "<br /><br />");
            Response.Write("getKey：" + getKey + "<br /><br />");
        }
    }
}