using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using ScalarApiLabs.Exceptions;
using ScalarApiLabs.Interfaces.Services;
using ScalarApiLabs.Models.Dto.Account;
using ScalarApiLabs.Models.Entities;
using ScalarApiLabs.Settings;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScalarApiLabs.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly JwtSettings jwtSettings;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<UserService> logger;

    public UserService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager,
        JwtSettings jwtSettings,
        IHttpContextAccessor httpContextAccessor,
        ILogger<UserService> logger)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.jwtSettings = jwtSettings;
        this.httpContextAccessor = httpContextAccessor;
        this.logger = logger;
    }

    public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue("uid") ?? string.Empty;

    public async Task<LoginResponseDto> Login(LoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException("Usuário não encontrado.");

        var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!passwordCheck.Succeeded)
        {
            throw new BusinessException("Credenciais inválidas.");
        }

        var jwtSecurityToken = await GenerateToken(user);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new LoginResponseDto
        {
            Token = token,
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty
        };
    }

    public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
    {
        var userExists = await userManager.FindByEmailAsync(request.Email);

        if (userExists is not null)
        {
            throw new BusinessException("Já existe um usuário com o e-mail informado.");
        }

        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());

            throw new CustomValidationException(errors);
        }

        var roleExists = await roleManager.RoleExistsAsync("Default");

        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole("Default"));
        }

        await userManager.AddToRoleAsync(user, "Default");

        var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
            };

        await userManager.AddClaimsAsync(user, claims);

        return new RegisterResponseDto { UserId = user.Id };
    }

    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(jwtSettings.Key));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new
        (
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }
}

