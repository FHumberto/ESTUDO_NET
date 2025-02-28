using Microsoft.AspNetCore.Mvc;

namespace ScalarApiLabs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Hello World!");
    }
}
