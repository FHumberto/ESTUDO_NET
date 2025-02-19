using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using T_Tier.BLL.DTOs.Users;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Wrappers;

namespace T_Tier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IUserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Tags = ["Auth"],
        Summary = "Autentica um usuário",
        Description = "Realiza autenticação de um usuário com as credenciais fornecidas e retorna um token de acesso."
    )]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await userService.Login(request);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
    }

    [Authorize]
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerOperation(
        Tags = ["Auth"],
        Summary = "Registra um usuário",
        Description = "Cadastra um novo usuário com base nos dados fornecidos com nível de permissão comum."
    )]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var response = await userService.Register(request);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.Conflict => Conflict(response),
            _ => BadRequest(response)
        };
    }

    [Authorize]
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Tags = new[] { "Account" },
        Summary = "Obtém um usuário pelo ID",
        Description = "Retorna os detalhes de um usuário pelo ID.")]
    public async Task<IActionResult> GetUserById([FromRoute] string userId)
    {
        var response = await userService.FindUserById(userId);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
    }

    [Authorize]
    [HttpGet("{userId}/posts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Tags = new[] { "Account" },
        Summary = "Obtém todas as postagens criadas pelo usuário a partir do ID",
        Description = "Retorna uma lista contendo os IDs de todas as postagens associadas ao usuário."
    )]
    public async Task<IActionResult> GetPostsWithUser([FromRoute] string userId)
    {
        var response = await userService.FindPostsWithUser(userId);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
    }

    [Authorize]
    [HttpGet("{userId}/comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Tags = new[] { "Account" },
        Summary = "Obtém todos os comentários criados pelo usuário a partir do ID",
        Description = "Retorna uma lista contendo os IDs de todos os comentários associadas ao usuário."
    )]
    public async Task<IActionResult> GetCommentsWithUser([FromRoute] string userId)
    {
        var response = await userService.FindCommentsWithUser(userId);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém todos os perfis de usuário cadastrados no sistema",
        Description = "Retorna uma lista com todos os perfils de usuário cadastrados no sistema.")]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await userService.GetAllRoles();

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            ResponseTypeEnum.Forbidden => Forbid(),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Remove um usuário", Description = "Remove um usuário com base no ID.")]
    public async Task<IActionResult> DeleteUser([FromRoute] string userId)
    {
        var response = await userService.DeleteUser(userId);

        return response.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(response),
            ResponseTypeEnum.InvalidInput => BadRequest(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{userId}/soft-delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Remove um usuário", Description = "Marca um usuário como deletado com base no ID.")]
    public async Task<IActionResult> SoftDeleteUser([FromRoute] string userId)
    {
        var response = await userService.SoftDeleteUser(userId);

        return response.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(response),
            ResponseTypeEnum.InvalidInput => BadRequest(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }
}