using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Threading.Tasks;
using U.Logging;

namespace U.Tests.Web.Logging
{
    public partial class log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var logger = new TestsLogger();
            //logger.Log(U.Logging.LogLevel.Debug, null, "log 1");
            //UPrime.Instance.Register<ILogger, TestsLogger>(U.Dependency.DependencyLifeStyle.Transient);
            //Thread.Sleep(1000);
            //var logger = UPrime.Instance.Resolve<ILogger>();
            //var a = 1;

            DateTime startDate = DateTime.Now;
            for (int i = 0; i <= 100000; i++)
            {
                var logger = UPrimeEngine.Instance.Resolve<ILogger>();
            }
            Response.Write((DateTime.Now - startDate).Milliseconds);
            //logger.Information("fATAL");
            //var a = 1;
            
        }
    }
}