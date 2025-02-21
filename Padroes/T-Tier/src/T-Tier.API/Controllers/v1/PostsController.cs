using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Wrappers;

namespace T_Tier.API.Controllers.v1;

[ApiVersion("1")]
public class PostsController(IPostService postService) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém todas as Postagens",
        Description = "Retorna uma lista com todas as postagens cadastradas.")]
    public async Task<IActionResult> GetAllPosts()
    {
        Response<IReadOnlyList<QueryPostResponseDto>>? response = await postService.GetAllPost();

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém uma postagem pelo ID",
        Description = "Retorna os detalhes de uma postagem pelo ID.")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var response = await postService.GetPostById(id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpGet("{id:int}/tags")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém uma postagem pelo ID e inclui suas tags.",
        Description = "Retorna os detalhes de uma postagem, incluindo uma lista de tags associadas a ela pelo ID.")]
    public async Task<IActionResult> GetPostWithTag(int id)
    {
        var response = await postService.GetPostByIdWithTag(id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpGet("{id:int}/comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém uma postagem pelo ID e inclui seus comentários.",
        Description = "Retorna os detalhes de uma postagem, incluindo uma lista de comentários associados a ela pelo ID.")]
    public async Task<IActionResult> GetPostWithComments(int id)
    {
        var response = await postService.GetPostByIdWithComments(id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerOperation(Summary = "Cadastra uma postagem",
        Description = "Cadastra uma postagem com base nos dados fornecidos, associada ao usuário atual.")]
    public async Task<IActionResult> CreatePost([FromBody] CommandPostRequestDto request)
    {
        var response = await postService.CreatePost(request);

        return response.Type switch
        {
            ResponseTypeEnum.Success => CreatedAtAction(nameof(CreatePost), new { id = response.Result }),
            ResponseTypeEnum.Conflict => Conflict(response),
            ResponseTypeEnum.InvalidInput => BadRequest(response.ValidationErrors),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Atualiza uma postagem",
        Description = "Atualiza uma postagem com base nos dados fornecidos.")]
    public async Task<IActionResult> UpdatePost([FromBody] CommandPostRequestDto request, int id)
    {
        var response = await postService.UpdatePost(request, id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(),
            ResponseTypeEnum.NotFound => NotFound(response),
            ResponseTypeEnum.InvalidInput => BadRequest(response.ValidationErrors),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Remove uma postagem",
        Description = "Remove uma postagem com base no ID.")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var response = await postService.DeletePostById(id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpDelete("{id:int}/soft-delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Remove uma postagem", Description = "Remove uma postagem com base no ID.")]
    public async Task<IActionResult> SoftDeletePost(int id)
    {
        var response = await postService.SoftDeletePostById(id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(response.Type),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }
}