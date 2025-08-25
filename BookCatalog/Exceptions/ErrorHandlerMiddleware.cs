﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookCatalog.Exceptions;

public class ErrorHandlerMiddleware(RequestDelegate _next, ILogger<ErrorHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                BaseException e => (int)e.StatusCode,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };
            var problemDetails = new ProblemDetails
            {
                Status = response.StatusCode,
                Title = error.Message,
            };
            logger.LogError(error.Message);
            var result = JsonSerializer.Serialize(problemDetails);
            await response.WriteAsync(result);
        }
    }
}