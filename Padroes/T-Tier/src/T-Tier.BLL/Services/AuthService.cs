using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using T_Tier.BLL.DTOs.Auth;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Settings;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    JwtSettings jwtSettings,
    IServiceProvider serviceProvider) : IAuthService
{
    public async Task<Response<LoginResponseDto>> Login(LoginRequestDto request)
    {
        var validationErrors = await serviceProvider.ValidateAsync(request);
        if (validationErrors.Count != 0) return new Response<LoginResponseDto>(null!, InvalidInput, validationErrors);

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) return new Response<LoginResponseDto>(null!, NotFound);

        var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!passwordCheck.Succeeded) return new Response<LoginResponseDto>(null!, Unauthorized, "Credenciais inválidas.");

        var jwtSecurityToken = await GenerateToken(user);

        return new Response<LoginResponseDto>(new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty
        }, Success);
    }


    public async Task<Response<RegisterResponseDto>> Register(RegisterRequestDto request)
    {
        var validationErrors = await serviceProvider.ValidateAsync(request);
        if (validationErrors.Count > 0)
            return new Response<RegisterResponseDto>(null!, InvalidInput, validationErrors);

        var userExists = await userManager.FindByEmailAsync(request.Email);

        if(userExists is not null)
            return new Response<RegisterResponseDto>
                (null!, Conflict, "Já exite um usuário com o e-mail informado.");

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new Response<RegisterResponseDto>(null!, InvalidInput, errors);
        }

        await userManager.AddToRoleAsync(user, "Default");

        return new Response<RegisterResponseDto>(new RegisterResponseDto { UserId = user.Id }, Success);
    }

    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!), new Claim("uid", user.Id)
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