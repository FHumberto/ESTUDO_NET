using Microsoft.AspNetCore.Mvc;

namespace ScalarApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonaController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Name = "John Doe", Age = 30 });
    }
}