using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using U.Logging;
using U.WebApi.Models;

namespace U.WebApi.Filters
{
    /// <summary>
    /// 过滤属性：异常处理
    /// </summary>
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            //记录日志
            LogHelper.Logger.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);


            //响应处理
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.OK);

            actionExecutedContext.Response.Content = new StringContent(new UResponseMessage(UResponseStatusCode.InternalServerError, 
                actionExecutedContext.Exception.Message).ToString());


            base.OnException(actionExecutedContext);

        }
    }
}
