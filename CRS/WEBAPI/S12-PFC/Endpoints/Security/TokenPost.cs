using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace S12_PFC.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    [AllowAnonymous] // permite que todos os users possam acessar.
    // método created
    public static IResult Action(LoginRequest loginRequest, IWebHostEnvironment environment, IConfiguration configuration, UserManager<IdentityUser> userManager, ILogger<TokenPost> log)
    {
        log.LogInformation("Getting Token");
        log.LogWarning("Warning");
        log.LogError("Error");

        // ENCONTRA O USUÁRIO POR E-MAIL
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        if (user == null)
            Results.BadRequest();

        // CHECA SE A SENHA BATE
        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            Results.BadRequest();

        var claims = userManager.GetClaimsAsync(user).Result;

        // CLAIM INFORMA O E-MAIL DO USUÁRIO
        var Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        });
        Subject.AddClaims(claims); // adiciona esses dadosao subject


        // RECEBE O ACESSO DA CONFIGURAÇÃO POR INJEÇÃO DE DEPENDÊNCIA
        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]); // CHAVE


        // TODAS AS INFORMAÇÕES CONTIDAS NO TOKEN
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = Subject, // ENVIA OS DADOS DO USUÁRIO JUNTO COM O TOKEN
            // ASSINATURA DA CREDENCIAL COM A KEY E O TIPO DE ALGORÍTIMO DE SEGURANÇA
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"],

            // VERIFICA O AMBIENTE, E CONFIGURA A DURAÇÃO DO TOKEN DE ACORDO.
            Expires = environment.IsDevelopment() || environment.IsStaging() ? DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(1),
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