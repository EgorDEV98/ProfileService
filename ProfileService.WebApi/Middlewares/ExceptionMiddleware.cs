using System.ComponentModel.DataAnnotations;
using System.Net;
using AppResponseExtension.Enums;
using AppResponseExtension.Response;

namespace ProfileService.WebApi.Middlewares;

public static class ExceptionMiddlewareExtention
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionMiddleware>();
}

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ExceptionResponse exception)
        {
            context.Response.StatusCode = (int)(exception.HttpStatusCode ?? HttpStatusCode.InternalServerError);
            
            var response = new BaseResponse()
            {
                Status = exception.Status,
                StatusMessage = exception.StatusMessage,
            };
            _logger.LogError(
                "(ResponseException:Status:{Status}, StatusMessage:{StatusMessage}",  response.Status, response.StatusMessage);
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var msg = $"{exception.GetType()}\n {exception.Message}\n {exception.InnerException?.Message}\n {exception.StackTrace}";
            var response = new BaseResponse()
            {
                Status = ResponseStatus.Error,
                StatusMessage = msg
            };
            _logger.LogError(msg);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}