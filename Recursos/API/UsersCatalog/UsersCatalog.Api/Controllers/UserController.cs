using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersCatalog.DataAccess.Data;

namespace UsersCatalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _db;

    public UserController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        return Ok(_db.Users.ToList());
    }
}
