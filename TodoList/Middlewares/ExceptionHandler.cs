using System.Net;
using System.Text.Json;

using TodoList.BLL.Exceptions;

namespace TodoList.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, ILoggerFactory loggerFactory)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ExceptionHandler>();

            httpContext.Response.Clear();
            httpContext.Response.ContentType = "text/plain";

            if (ex is AppExceptionBase apiException)
            {
                logger.LogError("Error '{stackTrace}'\nMessage '{messsage}'", apiException.StackTrace, apiException.Message);

                httpContext.Response.StatusCode = (int)apiException.Status.ToHttpStatusCode();
                await httpContext.Response.WriteAsync(apiException.Message);
            }
            else
            {
                logger.LogError(ex, "Unexpected exception");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsync(ex.Message);
            }
        }
    }
}
