using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CloudStates.API.Exceptions
{
    internal class CloudStatesExceptionHandler(IProblemDetailsService _problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                PersistenceException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            ProblemDetails details = new()
            {
                Type = exception.GetType().Name,
                Title = "Error",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            ProblemDetailsContext detailsContext = new()
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = details
            };

            return await _problemDetailsService.TryWriteAsync(detailsContext);
        }
    }
}
