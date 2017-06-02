using System;
using U.Web.Models;

namespace U.Web.Mvc.Models
{
    /// <summary>
    /// This class is used to create standard responses for ajax requests.
    /// </summary>
    [Serializable]
    public class MvcAjaxResponse<TResult> : AjaxResponse<TResult>
    {
        /// <summary>
        /// This property can be used to redirect user to a specified URL.
        /// </summary>
        public string TargetUrl { get; set; }

        public MvcAjaxResponse()
        {

        }

        public MvcAjaxResponse(bool success)
            : base(success)
        {

        }

        public MvcAjaxResponse(TResult result)
            : base(result)
        {

        }

        public MvcAjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}
