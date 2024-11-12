namespace RateLimiter;

public static class RateLimiterExtension
{
    /// <summary>
    /// Adiciona a politica de Rate Limiter configurada à coleção de serviços da aplicação.
    /// </summary>
    /// <param name="services">A coleção de serviços da aplicação.</param>
    /// <returns>A coleção de serviços da aplicação com a política de Reate Limiter adicionada.</returns>
    public static IServiceCollection AddRateLimiterPolicies(this IServiceCollection services)
    {

        return services;
    }
}
