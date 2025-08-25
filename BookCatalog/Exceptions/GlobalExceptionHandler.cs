using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookCatalog.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        switch (exception)
        {
            //case BaseException e:
            //    httpContext.Response.StatusCode = (int)e.StatusCode;
            //    problemDetails.Title = e.Message;
            //    break;

            case KeyNotFoundException e:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Title = e.Message;
                break;

            case UnauthorizedAccessException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                problemDetails.Title = "You are not authorized to access this resource.";
                break;

            case ArgumentException e:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = e.Message;
                break;

            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Title = "An unexpected error occurred.";
                break;
        }

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);
        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}