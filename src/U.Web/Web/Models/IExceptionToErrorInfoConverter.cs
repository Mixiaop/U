using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Web.Models
{
    public interface IExceptionToErrorInfoConverter
    {
        IExceptionToErrorInfoConverter Next { get; }

    }
}
