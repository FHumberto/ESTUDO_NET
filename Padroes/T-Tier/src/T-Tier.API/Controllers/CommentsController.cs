using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.BLL.Services;
using T_Tier.BLL.Wrappers;

namespace T_Tier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(CommentService commentService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém todos os comentários",
    Description = "Retorna uma lista com todos os comentários cadastrados.")]
    public async Task<IActionResult> GetAllComments()
    {
        Response<IReadOnlyList<QueryCommentDto>> query = await commentService.GetAllCommentAsync();
        return query.Type switch
        {
            ResponseTypeEnum.Success => Ok(query.Result),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
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
        Response<QueryCommentDto?> query = await commentService.GetCommentByIdAsync(id);
        return query.Type switch
        {
            ResponseTypeEnum.Success => Ok(query.Result),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Cadastra um comentário",
        Description = "Cadastra um comentário com base nos dados fornecidos.")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto request)
    {
        Response<int> command = await commentService.CreateCommentAsync(request);
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
        Response<bool> command = await commentService.UpdateCommentAsync(request, id);

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
        Response<bool> deleteTask = await commentService.DeleteAsync(id);
        return deleteTask.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
        };
    }
}