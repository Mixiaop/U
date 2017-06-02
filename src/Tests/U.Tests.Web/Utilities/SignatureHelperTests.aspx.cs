using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Security;

namespace U.Tests.Web.Utilities
{
    public partial class SignatureHelperTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sign = new PassSignature();
            var str = sign.Generate();
            Response.Write(str + "<br />");

            Response.Write(sign.Validation(str, 60));
        }
    }
}