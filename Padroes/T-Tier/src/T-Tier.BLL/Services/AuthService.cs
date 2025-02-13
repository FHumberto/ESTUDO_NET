using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using T_Tier.BLL.DTOs.Users;
using T_Tier.BLL.Settings;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager, JwtSettings jwtSettings)
{
    public async Task<Response<RegisterResponseDto?>> RegisterAsync(RegisterRequestDto request)
    {
        User user = new()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Employee");
            return new Response<RegisterResponseDto?>(new RegisterResponseDto() { UserId = user.Id });
        }
        else
        {
            StringBuilder str = new();

            foreach (IdentityError err in result.Errors)
            {
                str.AppendFormat("•{0}\n", err.Description);
            }

            return new Response<RegisterResponseDto?>(null, InvalidInput, [result.Errors.ToString()!]);
        }
    }

    public async Task<Response<AuthResponseDto?>> LoginAsync(AuthRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return new Response<AuthResponseDto?>(null, NotFound);
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            return new Response<AuthResponseDto?>(null, InvalidInput);
        }

        var jwtSecurityToken = await GenerateToken(user);

        AuthResponseDto response = new()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty
        };

        return new Response<AuthResponseDto?>(response, Success);
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
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(jwtSettings.Key));

        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}