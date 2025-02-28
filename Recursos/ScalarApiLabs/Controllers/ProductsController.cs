using Microsoft.AspNetCore.Mvc;

using ScalarApiLabs.Data.Repositories;
using ScalarApiLabs.Models.Dto;
using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    [HttpGet("Test")]
    public IActionResult Test()
    {
        return Ok("Hello World!");
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var response = await productRepository.GetAllAsync();
        return response.Any() ? Ok(response) : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var response = await productRepository.GetByIdAsync(id);
        return response is not null ? Ok(response) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCommandRequestDto request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        var response = await productRepository.AddAsync(product);

        return response
            ? CreatedAtAction(nameof(CreateProduct), request)
            : BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductCommandRequestDto request, int id)
    {
        var productExists = await productRepository.GetByIdAsync(id) is not null;

        if (!productExists)
        {
            return NotFound();
        }

        var product = new Product
        {
            Id = id,
            Name = request.Name,
            Price = request.Price
        };

        var response = await productRepository.UpdateAsync(product);

        return response
            ? Ok()
            : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var response = await productRepository.DeleteAsync(id);
        return response ? NoContent() : NotFound();
    }
}
