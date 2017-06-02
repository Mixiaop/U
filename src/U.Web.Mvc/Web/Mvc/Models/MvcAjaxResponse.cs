using System;
using U.Web.Models;

namespace U.Web.Mvc.Models
{
    /// <summary>
    /// This class is used to create standard responses for ajax requests.
    /// </summary>
    [Serializable]
    public class MvcAjaxResponse : MvcAjaxResponse<object>
    {
        public MvcAjaxResponse()
        {

        }

        public MvcAjaxResponse(bool success)
            : base(success)
        {

        }

        public MvcAjaxResponse(object result)
            : base(result)
        {

        }

        public MvcAjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}
