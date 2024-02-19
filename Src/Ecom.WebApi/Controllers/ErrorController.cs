using Ecom.WebApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Ecom.WebApi.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [NonAction]
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new CommonResponse(statusCode));
        }
    }
}
