using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U.Tests.Web.Extensions.String
{
    public partial class Base64 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("EncodeBase64：<br />");
            Response.Write("北京：" + ("北京").EncodeBase64() + "<br />");
            Response.Write("上海：" + ("上海").EncodeBase64() + "<br />");

            Response.Write("<br />DecodeBase64：<br />");
            Response.Write("北京：" + ("北京").EncodeBase64().DecodeBase64() + "<br />");
            Response.Write("上海：" + ("上海").EncodeBase64().DecodeBase64() + "<br />");

            Response.Write("<br /><br />EncodeUTF8Base64：<br />");
            Response.Write("北京：" + ("北京").EncodeUTF8Base64() + "<br />");
            Response.Write("上海：" + ("上海").EncodeUTF8Base64() + "<br />");

            Response.Write("<br />DecodeUTF8Base64：<br />");
            Response.Write("北京：" + ("北京").EncodeUTF8Base64().DecodeUTF8Base64() + "<br />");
            Response.Write("上海：" + ("上海").EncodeUTF8Base64().DecodeUTF8Base64() + "<br />");

        }
    }
}