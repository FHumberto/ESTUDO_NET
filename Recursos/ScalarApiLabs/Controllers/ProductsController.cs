using Microsoft.AspNetCore.Mvc;

using ScalarApiLabs.Data.Repositories;

namespace ScalarApiLabs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _productRepository;

    public ProductsController(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("Test")]
    public IActionResult Test()
    {
        return Ok("Hello World!");
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _productRepository.GetProductsAsync();
        return response.Any() ? Ok(response) : NotFound();
    }
}
