using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Tests.Web.Infrastructure;

namespace U.Tests.Web.Pages
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var startDate = DateTime.Now;
            int count = 1000;
            Response.Write("StartDate：" + startDate + "<br />");
            Response.Write("GetSettings：" + count);
            for (int i = 0; i < count;i++ ) {
                var settings = UPrimeEngine.Instance.Resolve<TestSettings>();
                Response.Write(i + "：" + settings.Name);
            }
            Response.Write("EndDate：" + DateTime.Now + "<br />");
            Response.Write("TotalMillsecond：" + (DateTime.Now - startDate).Milliseconds + "<br />");
        }
    }
}