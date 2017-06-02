using System;

namespace U.Web.Models
{
    /// <summary>
    /// This class used to create standed responses for ajax request
    /// </summary>
     [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        public AjaxResponse()
        {

        }

        public AjaxResponse(bool success)
            : base(success)
        {

        }

        public AjaxResponse(object result)
            : base(result)
        {

        }

        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}
