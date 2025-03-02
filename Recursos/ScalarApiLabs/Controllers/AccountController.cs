using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScalarApiLabs.Interfaces.Services;
using ScalarApiLabs.Models.Dto.Account;

namespace ScalarApiLabs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IUserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IResult> Login(LoginRequestDto request)
    => Results.Ok(await userService.Login(request));

    [Authorize]
    [HttpPost("Register")]
    public async Task<IResult> Register(RegisterRequestDto request)
        => Results.Ok(await userService.Register(request));
}
