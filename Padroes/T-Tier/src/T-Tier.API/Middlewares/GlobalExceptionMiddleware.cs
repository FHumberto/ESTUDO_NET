using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        int statusCode = StatusCodes.Status500InternalServerError;
        string errorMessage = "Erro interno inesperado de servidor";

        // Log resumido: apenas o tipo da exceção e a mensagem
        logger.LogError("exception: {ExceptionType} - {Message} | TraceId: {TraceId}",
            exception.GetType().Name,
            exception.Message,
            httpContext.TraceIdentifier);

        switch (exception)
        {
            case KeyNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                errorMessage = "O recurso requisitado não foi encontrado.";
                break;

            case UnauthorizedAccessException:
                statusCode = StatusCodes.Status403Forbidden;
                errorMessage = "Você não possui permissão para executar essa ação.";
                break;

            case ArgumentException or ArgumentNullException:
                statusCode = StatusCodes.Status400BadRequest;
                errorMessage = "Entrada inválida.";
                break;

            case InvalidOperationException:
                statusCode = StatusCodes.Status500InternalServerError;
                errorMessage = "Operação inválida.";
                break;
        }

        var response = new
        {
            status = statusCode,
            message = errorMessage
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);

        return true;
    }
}