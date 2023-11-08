using System.Net;
using FluentValidation;
using Newtonsoft.Json;

namespace API.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, 
        ILogger<ExceptionHandlerMiddleware> _logger)
    {
        _next = next;
        this._logger = _logger;
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

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error occurred while processing the request.");

        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string result = JsonConvert.SerializeObject(new ErrorDeatils
        {
            ErrorMessage = exception.Message,
            ErrorType = "Failure"
        });

        switch (exception)
        {

            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Errors);
                break;
            // Todo: Review
            // case BadRequestException badRequestException:
            //     statusCode = HttpStatusCode.BadRequest;
            //     break;
            // case NotFoundException notFoundException:
            //     statusCode = HttpStatusCode.NotFound;
            //     break;
            default:
                break;
        }

        
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(result);
    }
}

public class ErrorDeatils
{
    public string ErrorType { get; set; }
    public string ErrorMessage { get; set; }
}