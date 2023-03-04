﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using S12_PFC.Endpoints.Employees;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace S12_PFC.Endpoints.Security;

public static class TokenPost
{
    public static string Template => "/token"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    // método created
    public static IResult Action(LoginRequest loginRequest, EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        // ENCONTRA O USUÁRIO POR E-MAIL
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        if (user == null)
            Results.BadRequest();

        // CHECA SE A SENHA BATE
        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            Results.BadRequest();

        var key = Encoding.ASCII.GetBytes("A@fderwfQQSDXCCer34"); // CHAVE

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.Email)
            }),

            // ASSINATURA DA CREDENCIAL COM A KEY E O TIPO DE ALGORÍTIMO DE SEGURANÇA
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "PFC",
            Issuer = "Issuer"
        };

        // GERA O TOKEN
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}