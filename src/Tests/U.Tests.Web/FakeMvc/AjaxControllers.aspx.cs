using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Logging;
namespace U.Tests.Web.FakeMvc
{
    public partial class AjaxControllers : U.FakeMvc.UI.AjaxProGeneratePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //U.Logging.LogHelper.Logger.Error("AjaxControllers错了错了了了子");
        }
    }
}