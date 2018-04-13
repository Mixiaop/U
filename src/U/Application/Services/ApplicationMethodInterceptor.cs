using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Timers;
using System.Diagnostics;
using U.Timing;
using Castle.DynamicProxy;
using U.Startup;
using U.Logging;

namespace U.Application.Services
{
    /// <summary>
    /// 应用方法数据返回拦截（为null设默认值）
    /// </summary>
    public class ApplicationMethodInterceptor : IInterceptor
    {
        public ApplicationMethodInterceptor()
        {

        }

        public void Intercept(IInvocation invocation)
        {
            var settings = UPrimeEngine.Instance.Resolve<IUStartupConfiguration>().Settings;
            //if (settings.IsOpenedApplicationLogExceptionInterceptor)
            //{
            //    #region 
                var str = invocation.Method.Name + "_" + string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());
            try
            {
                invocation.Proceed();



            }
            catch (UException ex)
            {
                LogHelper.Logger.Error(str + " " + ex.Message);
                var aa = 1;
            }
            //}
            //else
            //{
            //    invocation.Proceed();
            //}
            #region Method running time
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            //invocation.Proceed();
            //watch.Stop();

            //var a = watch.ElapsedMilliseconds;
            //var b = 1;
            #endregion
        }
    }
}
