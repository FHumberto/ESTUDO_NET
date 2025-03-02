using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScalarApiLabs.Helpers;
using ScalarApiLabs.Interfaces.Persistence;
using ScalarApiLabs.Models.Dto.Product;
using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class ProductsController(IProductRepository productRepository)
{
    private readonly IProductRepository _repository = productRepository
        ?? throw new ArgumentNullException(nameof(productRepository));

    [HttpGet]
    public async Task<IResult> GetProducts([FromQuery] QueryFilters query)
    {
        var response = await _repository.GetAllAsync(query);
        return Results.Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetProductById(int id)
        => Results.Ok(await _repository.GetByIdAsync(id));

    [HttpPost]
    public async Task<IResult> CreateProduct([FromBody] ProductCommandRequestDto request)
    {
        var product = new Product { Name = request.Name, Price = request.Price };
        var created = await _repository.AddAsync(product);

        return Results.CreatedAtRoute(nameof(GetProductById), new { id = created }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateProduct(int id, [FromBody] ProductCommandRequestDto request)
    {
        var product = new Product { Id = id, Name = request.Name, Price = request.Price };
        await _repository.UpdateAsync(product);
        return Results.Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteProduct(int id)
    {
        await _repository.DeleteAsync(id);
        return Results.NoContent();
    }
}
