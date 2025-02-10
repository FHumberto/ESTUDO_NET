#region

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.BLL.Services;
using T_Tier.BLL.Wrappers;

#endregion

namespace T_Tier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController(PostService postService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obter todas as Postagens",
        Description = "Retorna uma lista de Postagens.")]
    public async Task<IActionResult> GetAllPosts()
    {
        Response<IReadOnlyList<QueryPostDto>> query = await postService.GetAllPostAsync();

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
    [SwaggerOperation(Summary = "Obter uma Postagens pelo Id",
        Description = "Retorna os detalhes de uma Postagem pelo Id.")]
    public async Task<IActionResult> GetPostById(int id)
    {
        Response<QueryPostDto?> query = await postService.GetPostByIdAsync(id);

        return query.Type switch
        {
            ResponseTypeEnum.Success => Ok(query.Result),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
        };
    }

    [HttpGet("{id:int}/tags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obter uma Postagem pelo Id e inclui suas tags.",
        Description = "Retorna os detalhes de uma Postagem, incluindo uma lista de Tags associadas a ele pelo Id.")]
    public async Task<IActionResult> GetPostWithTag(int id)
    {
        Response<QueryPostTagDto?> query = await postService.GetPostByIdWithTagAsync(id);

        return query.Type switch
        {
            ResponseTypeEnum.Success => Ok(query.Result),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
        };
    }

    //TODO: REVISAR ESSE ENDPOINT QUANDO ADICIONAR USERS
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Cadastrar uma Postagem",
        Description = "Cadastra uma Postagem com base nos dados fornecidos associado ao usuario atual.")]
    public async Task<IActionResult> CreatePost([FromBody] CommandPostDto request)
    {
        Response<int> command = await postService.CreatePostAsync(request);

        return command.Type switch
        {
            ResponseTypeEnum.Success => CreatedAtAction(nameof(CreatePost), new { id = command.Result },
                new { id = command.Result }),
            _ => BadRequest(command.Errors)
        };
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Atualizar uma Postagem",
        Description = "Atualiza uma Postagem com base nos dados fornecidos.")]
    public async Task<IActionResult> UpdatePost([FromBody] CommandPostDto request, int id)
    {
        Response<bool> command = await postService.UpdatePostAsync(request, id);

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
    [SwaggerOperation(Summary = "Remover uma Postagem", Description = "Remove um Postagem com base no ID.")]
    public async Task<IActionResult> DeletePost(int id)
    {
        Response<bool> deleteTask = await postService.DeleteAsync(id);

        return deleteTask.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
        };
    }
}