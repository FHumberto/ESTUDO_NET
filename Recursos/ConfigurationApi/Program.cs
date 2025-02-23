using ConfigurationApi.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adiciona no inicio do projeto
builder.Services.Configure<InfraSettings>
    (builder.Configuration.GetSection("InfraSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options
        => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Configuration API"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
