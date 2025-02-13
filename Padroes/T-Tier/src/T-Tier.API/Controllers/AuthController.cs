using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using T_Tier.BLL.DTOs.Users;
using T_Tier.BLL.Services;
using T_Tier.BLL.Wrappers;

namespace T_Tier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Autentica um usuário",
        Description = "Realiza autenticação de um usuário com as credenciais fornecidas e retorna um token de acesso.")]
    public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
    {
        var response = await authService.LoginAsync(request);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest(response.Errors)
        };
    }

    [Authorize]
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Registra um usuário",
        Description = "Cadastra um novo usuário com base nos dados fornecidos com nível de permissão comum.")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var response = await authService.RegisterAsync(request);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(request),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest(response.Errors)
        };
    }
}