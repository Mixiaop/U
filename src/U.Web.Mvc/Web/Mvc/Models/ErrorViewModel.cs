using System;
using U.Web.Models;

namespace U.Web.Mvc.Models
{
    public class ErrorViewModel
    {
        public ErrorInfo ErrorInfo { get; set; }

        public Exception Exception { get; set; }

        public ErrorViewModel() { }

        public ErrorViewModel(Exception exception) {
            Exception = exception;
            ErrorInfo = new ErrorInfo(0, exception.Message, exception.ToString());
        }
    }
}
