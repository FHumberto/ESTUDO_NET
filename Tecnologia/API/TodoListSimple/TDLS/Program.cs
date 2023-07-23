using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TDLS.Data;
using TDLS.EndPoints.TodoItem;

var builder = WebApplication.CreateBuilder(args);
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Todo List Simples",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Humberto Guedes",
            Email = "fhumberto.trab@gmail.com",
            Url = new Uri("https://fhumberto.dev.br")
        }
    });

    var xmlFile = "TDLS.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddDbContext<TdlsContext>(options => options.UseSqlServer(connectionStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(TodoItemPost.Template, TodoItemPost.Methods, TodoItemPost.Handle).WithTags("Todo"); // ROTA CREATE
app.MapMethods(TodoItemGetAll.Template, TodoItemGetAll.Methods, TodoItemGetAll.Handle).WithTags("Todo"); // ROTA READ
app.MapMethods(TodoItemPut.Template, TodoItemPut.Methods, TodoItemPut.Handle).WithTags("Todo"); // ROTA UPDATE
app.MapMethods(TodoItemDelete.Template, TodoItemDelete.Methods, TodoItemDelete.Handle).WithTags("Todo"); // ROTA DELETE

app.Run();