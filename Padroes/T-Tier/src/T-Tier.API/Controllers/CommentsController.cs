using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Wrappers;

namespace T_Tier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CommentsController(ICommentService commentService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém todos os comentários",
    Description = "Retorna uma lista com todos os comentários cadastrados.")]
    public async Task<IActionResult> GetAllComments()
    {
        var response = await commentService.GetAllComment();
        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            ResponseTypeEnum.Forbidden => Forbid(),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém um comentário pelo ID",
        Description = "Retorna os detalhes de um comentário pelo ID, incluindo a postagem e o usuário.")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        var response = await commentService.GetCommentById(id);
        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Cadastra um comentário",
        Description = "Cadastra um comentário com base nos dados fornecidos.")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto request)
    {
        var command = await commentService.CreateComment(request);
        return command.Type switch
        {
            ResponseTypeEnum.Success => CreatedAtAction(nameof(CreateComment), new { id = command.Result },
                new { id = command.Result }),
            _ => BadRequest(command.Errors)
        };
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Atualiza um comentário",
        Description = "Atualiza um comentário com base nos dados fornecidos.")]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto request, int id)
    {
        var command = await commentService.UpdateComment(request, id);

        return command.Type switch
        {
            ResponseTypeEnum.Success => Ok(),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest(command.Errors)
        };
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Remove um comentário", Description = "Remove um comentário com base no ID.")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var deleteTask = await commentService.DeleteComment(id);
        return deleteTask.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
        };
    }
}