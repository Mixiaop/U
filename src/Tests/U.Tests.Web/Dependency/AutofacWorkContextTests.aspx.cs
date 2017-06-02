using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;

namespace U.Tests.Web.Dependency
{
    public partial class AutofacWorkContextTests : AutofacPageBase
    {
        IWorkContext _workContext = UPrimeEngine.Instance.Resolve<IWorkContext>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}