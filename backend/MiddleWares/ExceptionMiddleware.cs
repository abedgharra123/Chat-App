using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace backend.MiddleWares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Pass the request to the next middleware
            await next(context);
        }
        catch (Exception ex)
        {
            // Handle exceptions globally
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception here (e.g., use a logger)
        logger.LogError(exception, exception.Message);
        // Customize the response
        var response = new
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "An unexpected error occurred.",
            Detail = env.IsDevelopment() ? exception.Message : "Internal Server Error" 
       };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
