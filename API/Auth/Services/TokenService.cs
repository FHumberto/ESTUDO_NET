
using Auth.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[]? key = Encoding.ASCII.GetBytes(_configuration["JwtTokenSettings:Key"]!);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name!), // User.Identity.Name
                new Claim(ClaimTypes.Role, user.Role!), // User.IsInRole
            }),

            Expires = DateTime.UtcNow.AddHours(8), // duração do token
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), // chave única para decriptar o token
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
