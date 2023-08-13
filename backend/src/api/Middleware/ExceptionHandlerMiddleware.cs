using System.Net;

namespace cfg.api.Middleware;

public sealed class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var message = "Internal Server Error!";
        if (exception is ArgumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            message = exception.Message;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        
        await context.Response.WriteAsync(new 
        {
            context.Response.StatusCode,
            message
        }.ToString() ?? "unspecified error");
    }
}