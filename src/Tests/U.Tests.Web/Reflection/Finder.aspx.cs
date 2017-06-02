using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Reflection;

namespace U.Tests.Web.Reflection
{
    public partial class Finder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IAssemblyFinder finder = new AppDomainAssemblyFinder();
            var assemblies = finder.GetAllAssemblies();
           // var a = 1;
        }
    }
}