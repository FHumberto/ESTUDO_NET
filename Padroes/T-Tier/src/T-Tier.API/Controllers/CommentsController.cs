using Microsoft.AspNetCore.Mvc;
using T_Tier.BLL.Services;

namespace T_Tier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(CommentsService commentsService) : ControllerBase
{
    public IActionResult Index()
    {
        return Ok();
    }
}