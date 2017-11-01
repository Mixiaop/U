using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Compress;

namespace U.Tests.Web.Utilities
{
    public partial class ZipHelperTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZipHelper.ZipDirectory(@"D:\!Github\U\src\Tests\U.Tests.Web\USegmentation\Resources\", U.Utilities.Web.WebHelper.MapPath("/Utilities/"), "article");
        }
    }
}