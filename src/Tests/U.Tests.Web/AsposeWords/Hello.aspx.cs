using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Converters;
namespace U.Tests.Web.AsposeWords
{
    public partial class Hello : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IConverterService converterService = U.UPrimeEngine.Instance.Resolve<IConverterService>();
            converterService.HtmlToImage("http://192.168.1.165:14480/views/tests/careerAnchor/results.html", 800, 600, Request.MapPath("/AsposeWords/cc.jpg"));
        }
    }
}