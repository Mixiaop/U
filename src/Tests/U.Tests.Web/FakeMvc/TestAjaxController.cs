using AjaxPro;
using U.FakeMvc.Controllers;

namespace U.Tests.Web.FakeMvc
{
    [AjaxNamespace("UserService")]
    public class TestAjaxController : AjaxControllerBase
    {

        [AjaxMethod]
        public string SayHi()
        {
            return "hello world";
        }
    }
}