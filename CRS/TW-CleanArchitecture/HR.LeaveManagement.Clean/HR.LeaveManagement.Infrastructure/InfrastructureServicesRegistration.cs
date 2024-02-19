using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //? pega uma seção do arquivo de configuração
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        //? transient para criar uma nova instância a cada vez que é usado
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}
