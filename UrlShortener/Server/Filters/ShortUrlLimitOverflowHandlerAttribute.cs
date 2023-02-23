using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Server.Filters
{
    public class ShortUrlLimitOverflowHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var error = new ProblemDetails
            {
                Title = "ShortUrlLimitOverflowHandlerException",
                Detail = context.Exception.Message,
                Status = 500,
            };
            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
