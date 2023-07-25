using Microsoft.AspNetCore.Mvc;
using ProductHub_Infra.Data;

namespace ProductHub_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Get()
    {
        if(_db.Products.ToList() is not null)
        {
            return Ok(_db.Products.ToList());
        }
        else
        {
            return NoContent();
        }
    }
}
