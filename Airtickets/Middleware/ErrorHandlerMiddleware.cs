using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Npgsql;

namespace Airtickets.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            await CreateHandleResponseAsync(context, HttpStatusCode.BadRequest, exception);
        }
        catch (InvalidOperationException exception)
        {
            await CreateHandleResponseAsync(context, HttpStatusCode.Conflict, exception);
        }
        catch (AggregateException exception)
        {
            await CreateHandleResponseAsync(context, HttpStatusCode.RequestEntityTooLarge, exception);
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is PostgresException npgex)
                switch (npgex.SqlState)
                {
                    case PostgresErrorCodes.UniqueViolation:
                        await CreateHandleResponseAsync(context, HttpStatusCode.Conflict, exception);
                        break;
                    case PostgresErrorCodes.InvalidTransactionState:
                        await CreateHandleResponseAsync(context, HttpStatusCode.RequestTimeout, exception);
                        break;
                    default:
                        await CreateHandleResponseAsync(context, HttpStatusCode.InternalServerError, exception);
                        break;
                }
            else
                await CreateHandleResponseAsync(context, HttpStatusCode.InternalServerError, exception);
        }
        catch (Exception exception)
        {
            await CreateHandleResponseAsync(context, HttpStatusCode.InternalServerError, exception);
        }
    }

    private async Task<HttpResponse> CreateHandleResponseAsync(HttpContext context, HttpStatusCode code,
        Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)code;
        var result = JsonSerializer.Serialize(new { message = exception?.Message });
        await response.WriteAsync(result);
        return response;
    }
}