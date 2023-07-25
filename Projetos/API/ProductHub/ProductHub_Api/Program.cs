using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Version = "v1",
        Title = "Product Hub",
        Description = "Sistema de catalogo e gerenciamento de produto simples. Para estudo de tecnicas avançadas com .net.",
        Contact = new OpenApiContact
        {
            Name = "Humberto Guedes",
            Email = "fhumberto.trab@gmail.com",
            Url = new Uri(Environment.GetEnvironmentVariable("Contact") ?? "Arquivo de configuração não encontrado"),
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
