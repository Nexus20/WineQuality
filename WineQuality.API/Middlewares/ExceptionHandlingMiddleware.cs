using WineQuality.Application.Exceptions;

namespace WineQuality.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            logger.LogInformation(exception, "Validation exception is occured");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(exception.Message);
        }
        catch (IdentityException exception)
        {
            logger.LogInformation(exception, "Identity exception is occured");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (NotFoundException exception)
        {
            logger.LogInformation(exception, "Resource not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        catch (Exception exception)
        {
            HandleStatus500Exception(context, exception, logger);
        }
    }

    private void HandleStatus500Exception(HttpContext context, Exception exception, ILogger logger)
    {
        logger.LogError(exception, "An exception was thrown as a result of the request");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }
}