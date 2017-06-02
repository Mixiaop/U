using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using U.Reflection;
using U.Logging;
using U.Web.Models;
using U.Web.Mvc.Models;
using U.Web.Mvc.Controllers.Results;

namespace U.Web.Mvc.Controllers
{
    /// <summary>
    /// Base class for al MVC controllers in U system.
    /// </summary>
    public abstract class UConroller : Controller
    {
        protected ILogger Logger { get; set; }

        /// <summary>
        /// MethodInfo for currently executing action.
        /// </summary>
        private MethodInfo _currentMethodInfo;

        /// <summary>
        /// WrapResultAttribute for currently executing action.
        /// </summary>
        private WrapResultAttribute _wrapResultAttribute;

        public UConroller()
        {
            Logger = LogHelper.Logger;
        }

        #region OnActionExecuting / OnActionExecuted

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetCurrentMethodInfoAndWrapResultAttribute(filterContext);

            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        private void SetCurrentMethodInfoAndWrapResultAttribute(ActionExecutingContext filterContext)
        {
            _currentMethodInfo = ActionDescriptorHelper.GetMethodInfo(filterContext.ActionDescriptor);
            _wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrNull<WrapResultAttribute>(_currentMethodInfo) ??
                WrapResultAttribute.Default;
        }

        #endregion

        #region Exception handling
        protected override void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //If exception handled before, do nothing.
            //If this is child action, exception should be handled by main action.
            if (context.ExceptionHandled || context.IsChildAction)
            {
                base.OnException(context);
                return;
            }

            //Log exception
            if (_wrapResultAttribute.LogError)
            {
                Logger.Error(context.Exception, context.Exception.Message);
            }

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (!context.HttpContext.IsCustomErrorEnabled)
            {
                base.OnException(context);
                return;
            }

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, context.Exception).GetHttpCode() != 500)
            {
                base.OnException(context);
                return;
            }

            //Check WrapResultAttribute
            if (!_wrapResultAttribute.WrapOnError)
            {
                base.OnException(context);
                return;
            }

            //We handled the exception!
            context.ExceptionHandled = true;

            //Return a special error response to the client.
            context.HttpContext.Response.Clear();
            context.Result = IsJsonResult()
                ? GenerateJsonExceptionResult(context)
                : GenerateNonJsonExceptionResult(context);

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            context.HttpContext.Response.TrySkipIisCustomErrors = true;

        }

        protected virtual bool IsJsonResult()
        {
            return typeof(JsonResult).IsAssignableFrom(_currentMethodInfo.ReturnType) ||
                   typeof(Task<JsonResult>).IsAssignableFrom(_currentMethodInfo.ReturnType);
        }

        protected virtual ActionResult GenerateJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500; //TODO: also be  return 200
            return new UJsonResult(
                new MvcAjaxResponse(
                    new ErrorInfo(500, context.Exception.Message, context.Exception.ToString()),
                    false
                    )
                );
        }

        protected virtual ActionResult GenerateNonJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(context.Exception)),
                TempData = context.Controller.TempData
            };
        }
        #endregion
    }
}
