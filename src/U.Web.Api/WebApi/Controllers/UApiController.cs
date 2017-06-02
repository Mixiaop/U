using System.Web.Http;
using U.WebApi.Filters;

namespace U.WebApi.Controllers
{
    [HandleException]
    public abstract class UApiController : ApiController
    {
    }
}
