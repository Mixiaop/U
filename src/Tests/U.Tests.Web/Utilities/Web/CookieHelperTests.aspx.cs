using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Web;
namespace U.Tests.Web.Utilities.Web
{
    public partial class CookieHelperTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSetCookie.Click += btnSetCookie_Click;
            btnGetCookie.Click += btnGetCookie_Click;
        }

        void btnGetCookie_Click(object sender, EventArgs e)
        {
            Response.Write(CookieHelper.GetCookieValue("Youzy.Hello"));
            
        }

        void btnSetCookie_Click(object sender, EventArgs e)
        {
            CookieHelper.SetCookie("Youzy.Hello", "world");
            Response.Redirect("http://www.baidu.com");
        }
    }
}