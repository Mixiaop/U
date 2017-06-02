using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Logging;
namespace U.Tests.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 100000; i++)
            {
                var logger = UPrimeEngine.Instance.Resolve<ILogger>();
            }
            Response.Write("finished");
        }
    }
}