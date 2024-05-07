using AccumenSalesActivity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AccumenSalesActivity.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        [HttpGet]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ErrorViewModel model = new();

            switch (statusCode)
            {
                case 404:
                    model.StatusCode = 404;
                    model.Message = "Not Found";
                    model.Details = "Sorry, the resources you requested could not be found!";
                    break;
                case 400:
                    model.StatusCode = 400;
                    model.Message = "Bad Request";
                    model.Details = "Sorry, the request cannot be process!";
                    break;

                case 401:
                    model.StatusCode = 401;
                    model.Message = "Unauthorized";
                    model.Details = "Sorry, the request cannot be process!";
                    break;

                default:
                    model.StatusCode = 0;
                    break;
            }

            return View(model);
        }

        [Route("Error")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ExceptionViewModel returnModel = new()
            {
                ExceptionPath = exceptionDetails.Path,
                ExceptionMessage = exceptionDetails.Error.Message,
                StackTrace = exceptionDetails.Error.StackTrace
            };
            return View(returnModel);
        }

    }
}
