using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Strings;
namespace U.Tests.Web.Utilities
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            Response.Write((DateTime.Now.Ticks.ToString() + StringHelper.GenerateRandomString(64)).EncodeBase64());

        }
    }
}