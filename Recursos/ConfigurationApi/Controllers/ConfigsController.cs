using Microsoft.AspNetCore.Mvc;

namespace ConfigurationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigsController : ControllerBase
{
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}