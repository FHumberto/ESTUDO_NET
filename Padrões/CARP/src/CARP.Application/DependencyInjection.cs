using Microsoft.Extensions.DependencyInjection;

namespace CARP.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services;
}
