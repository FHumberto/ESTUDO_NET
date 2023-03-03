using Microsoft.EntityFrameworkCore;

using TDLS.Data;
using TDLS.EndPoints.TodoItem;

var builder = WebApplication.CreateBuilder(args);
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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