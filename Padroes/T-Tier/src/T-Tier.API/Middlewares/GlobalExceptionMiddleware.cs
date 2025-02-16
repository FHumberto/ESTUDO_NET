using Microsoft.AspNetCore.Diagnostics;
using T_Tier.BLL.Wrappers;

public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        int statusCode = StatusCodes.Status500InternalServerError;
        var responseType = ResponseTypeEnum.Error;
        string errorMessage = "Erro interno inesperado de servidor";

        logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        switch (exception)
        {
            case KeyNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                responseType = ResponseTypeEnum.NotFound;
                errorMessage = "O recuso requisitado não foi encontrado.";
                break;

            case UnauthorizedAccessException:
                statusCode = StatusCodes.Status403Forbidden;
                responseType = ResponseTypeEnum.Forbidden;
                errorMessage = "Você não possui permissão para performar essa ação.";
                break;

            case ArgumentException or ArgumentNullException:
                statusCode = StatusCodes.Status400BadRequest;
                responseType = ResponseTypeEnum.InvalidInput;
                errorMessage = "Entrada Inválida.";
                break;
        }

        var response = new Response<bool>(false, responseType, new List<string> { errorMessage });

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
