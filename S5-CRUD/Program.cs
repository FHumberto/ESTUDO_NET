using Microsoft.AspNetCore.Mvc;

using S5_CRUD.Data;
using S5_CRUD.Model;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// CREATE
app.MapPost("/products", (Product product) =>
{
    ProductRepository.Add(product);
    return Results.Created($"/products/{product.Code}", product.Code);
});

// READ
app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    if (product != null)
    {
        Results.Ok(product);
    }
    else
    {
        Results.NotFound();
    }
});

// EDIT
app.MapPut("/products", (Product product) =>
{
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok();
});

// DELETE
app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    ProductRepository.Remove(product);
    return Results.Ok();
});

app.Run();