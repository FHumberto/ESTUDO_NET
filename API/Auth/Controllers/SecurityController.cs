using Auth.Data;
using Auth.Models.Entities;
using Auth.Models.ViewModels;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Auth.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SecurityController : ControllerBase
{
    private readonly AuthDbContext _dbContext;
    private readonly TokenService _tokenService;

    public SecurityController(AuthDbContext dbContext, TokenService tokenService)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string nickName, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.NickName == nickName && u.Password == password);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(_tokenService.GenerateToken(user));
    }
}
