using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRateLimiter(options =>
{
    //politica definida para os que tem data anotation
    ////? configuração da Estratégia Escolhida, com base na política
    //options.AddFixedWindowLimiter(policyName: "Fixed", config =>
    //{
    //    config.PermitLimit = 100; //* número máximo de requisições por janela
    //    config.Window = TimeSpan.FromMinutes(1); //* duração da janela (1 minuto)
    //    config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; //* ordem de processamento (ordem de chegada)
    //    config.QueueLimit = 2; //* limite de 2 requisições na fila de espera
    //});

    //politica padrão que é aplicada a todos os controller
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>
    (httpContext => RateLimitPartition.GetFixedWindowLimiter("Fixed", _
        => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst, // Ordem de processamento (ordem de chegada)
            QueueLimit = 2 // Limite de 2 requisições na fila de espera
        }));

    //? personaliza a resposta de rejeição
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsJsonAsync(
            new { message = "Você excedeu o limite de requisições. Tente novamente mais tarde." },
            cancellationToken: cancellationToken);
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers()
    .RequireRateLimiting("Fixed");

app.Run();
