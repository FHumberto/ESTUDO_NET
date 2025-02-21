using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace T_Tier.API.Controllers;
[ApiController]
[Authorize]
[Route("api/v{v:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
}