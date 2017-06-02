using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Utilities.Words;

namespace U.Tests.Web.Utilities
{
    public partial class WordHelperTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var items = PinyinHelper.ToPinyin("项目地址传送门继完成了");
            Response.Write(items);
        }
    }

}