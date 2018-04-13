using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace U.Tests.Application
{
    [TestClass]
    public class MethodReturnInterceptor_Tests : TestsBase
    {
        [TestMethod]
        public void Testing() {
            //var a = UPrimeEngine.Instance.Resolve<UserService>().GetName();

            //Console.WriteLine(a);
        }

        public class UserService : U.Application.Services.IApplicationService {
            public string GetName() {
                return "heyZben";
            }
        }
    }
}
