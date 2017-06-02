using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U.Tests.Web.Jobs
{
    public class SendEmailJobArgs
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }


    }
}