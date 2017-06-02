using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using U.Utilities.Security;
using U.Utilities.Web;

namespace U.Tests.Web.Utilities
{
    public partial class encryption : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(WebHelper.GetString("page").Replace(" ", "+"));
            Response.Write("<br />");
            //Response.Write(new ASCIIEncoding().GetBytes("youzycn125123512"));
            //Response.Write(EncriptionHelper.EncryptText("youzy.cn1", "youzycn19"));

            Response.Write(EncriptionHelper.Encrypt("page=1", "youzy"));
            Response.Write("<br />");
            Response.Write(EncriptionHelper.Decrypt(WebHelper.GetString("page").Replace(" ", "+"), "youzy"));
        }
    }
}