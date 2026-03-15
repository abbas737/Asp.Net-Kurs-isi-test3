using System.Net;
using System.Text.Json;
using Tank_Wiki.Common;

namespace Tank_Wiki.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ApiResponse<string>
        {
            Success = false,
            Message = exception.Message,
            Data = null
        };

        var json = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(json);
    }
}
