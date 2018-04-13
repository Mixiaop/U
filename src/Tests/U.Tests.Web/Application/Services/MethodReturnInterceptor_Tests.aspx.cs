using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using U.Runtime.Caching;
namespace U.Tests.Web.Application.Services
{
    public partial class MethodReturnInterceptor_Tests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var name = UPrimeEngine.Instance.Resolve<UserService>().GetName();
            Response.Write(name);
        }

        public interface IUserService : U.Application.Services.IApplicationService {
            string GetName();
        }
        public class UserService : IUserService
        {
            public virtual string GetName()
            {
                UserTest user = null;
                var a = user.Id;
                //Thread.Sleep(1000);
                return "heyZben";
            }
        }

        public class UserTest
        {
            public int Id { get; set; }
        }
    }
}