using ScalarApiLabs.Exceptions;

using System.Net;
using System.Text.Json;

namespace ScalarApiLabs.Middlewares;

public sealed class GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<GlobalExceptionHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var operation = context.Request.Method;
        var path = context.Request.Path.Value ?? "UnknownPath";

        //? teste de estruturação de log com caminho, operação e tipo de exceção
        _logger.LogError("API:{Path}:{Operation}: Exception {ExceptionType} - {ErrorMessage}",
                         path, operation, ex.GetType().Name, ex.Message);

        var (status, message) = ex switch
        {
            NotFoundException => (HttpStatusCode.NotFound, ex.Message),
            BusinessException => (HttpStatusCode.BadRequest, ex.Message),
            DatabaseException => (HttpStatusCode.InternalServerError, "Erro de banco de dados. Tente novamente mais tarde."),
            ArgumentNullException => (HttpStatusCode.InternalServerError, "Falha na configuração do servidor."),
            _ => (HttpStatusCode.InternalServerError, "Erro interno no servidor.")
        };

        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var response = JsonSerializer.Serialize(new { error = message });
        await context.Response.WriteAsync(response);
    }
}
