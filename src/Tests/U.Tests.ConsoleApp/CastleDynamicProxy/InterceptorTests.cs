using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace U.Tests.ConsoleApp.CastleDynamicProxy
{
    public class InterceptorTests
    {
        public void Run() {
            var simpleClass = new InterceptedClass();
            var interceptor = new Interceptor();

            var cp = new ProxyGenerator().CreateClassProxyWithTarget(simpleClass, interceptor);

            cp.Method1();
            cp.Method2();

            //Console.ReadLine();
        }
    }

    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine(string.Format("Intercepted call to: " + invocation.Method.Name));

            invocation.Proceed();
        }
    }

    public class InterceptedClass
    {
        public virtual void Method1()
        {
            Console.WriteLine("Called Method 1");
            Method2();
        }

        public virtual void Method2()
        {
            Console.WriteLine("Called Method 2");
        }
    }
}
