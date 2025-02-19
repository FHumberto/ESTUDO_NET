using Microsoft.Extensions.Options;
using Serilog;
using T_Tier.API.Extensions;
using T_Tier.BLL;
using T_Tier.BLL.Settings;
using T_Tier.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);
builder.Services.RegisterBLLDependencies(builder.Configuration);
builder.Services.RegisterDALDependencies(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionMiddleware>();
builder.Services.AddCorsPolicies();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

builder.Services.AddSwaggerServices();

builder.Services.AddAuthenticationServices(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCorsPolicies();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json",
            "Three Layer API v1");
    });
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();