using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JetBrains.Annotations;

namespace U.Tests.Web
{
    public partial class JetBrainsAnnotationsTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HelloClass hello = new HelloClass() ;
            hello.Name = null;
        }
    }

    public class HelloClass {
        [NotNull]
        public string Name { get; set; }
    }
}