using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace QuickbooksConnector.Api.Filters;

public sealed class HttpExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ILogger<HttpExceptionFilter> _logger;

    public HttpExceptionFilter(ILogger<HttpExceptionFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public int Order => int.MaxValue - 10;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null) return;

        var request = context.HttpContext.Request;

        switch (context.Exception)
        {
            case ArgumentException argumentException:
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "Invalid argument",
                    Details = argumentException.Message
                });
                context.ExceptionHandled = true;
                break;

            case BadHttpRequestException badHttpRequestException:
                context.Result = new BadRequestObjectResult(new
                {
                    Error = "Bad request",
                    Details = badHttpRequestException.Message
                });
                context.ExceptionHandled = true;
                break;

            case InvalidOperationException invalidOperationException:
                context.Result = new ObjectResult(new
                {
                    Error = "Unprocessable entity",
                    Details = invalidOperationException.Message
                })
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                };
                context.ExceptionHandled = true;
                break;

            case COMException comException:
                context.Result = new ObjectResult(new
                {
                    Error = "Internal server error",
                    Details = comException.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                context.ExceptionHandled = true;
                break;

            default:
                _logger.LogError(
                    context.Exception,
                    $"[{request.Method}] {request.Scheme}://{request.Host}{request.Path}{request.QueryString} failed with error message: {context.Exception.Message}"
                );
                context.Result = new ObjectResult(new
                {
                    Error = "Internal server error",
                    Details = context.Exception.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                context.ExceptionHandled = true;
                break;
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}

