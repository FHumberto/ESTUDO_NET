using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Wrappers;

namespace T_Tier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagsController(ITagService tagService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obter todas as tags", Description = "Retorna uma lista com todas as Tags cadastradas.")]
    public async Task<IActionResult> GetAllTags()
    {
        var response = await tagService.GetAllTag();

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
    [SwaggerOperation(Summary = "Obter uma Tag pelo Id", Description = "Retorna uma Tag específica com base no ID.")]
    public async Task<IActionResult> GetTag(int id)
    {
        var response = await tagService.GetTagById(id);

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
    [SwaggerOperation(Summary = "Cadastrar uma Tag", Description = "Cadastra uma Tag com base nos dados fornecidos.")]
    public async Task<IActionResult> CreateTag([FromBody] CommandTagDto request)
    {
        var response = await tagService.CreateTag(request);

        return response.Type switch
        {
            ResponseTypeEnum.Success => CreatedAtAction(nameof(CreateTag), new { id = response.Result }),
            ResponseTypeEnum.Conflict => Conflict(response),
            ResponseTypeEnum.InvalidInput => BadRequest(response.ValidationErrors),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpPatch("{postId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Adiciona tags a uma postagem pelo ID",
        Description = "Adiciona tags a uma postagem com base nos IDs das Tags.")]
    public async Task<IActionResult> AddTagToPost([FromBody] CommandAddTagPostRequest request, int postId)
    {
        var response = await tagService.AddTagToPost(request, postId);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            ResponseTypeEnum.InvalidInput => BadRequest(response.ValidationErrors),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Atualizar uma Tag", Description = "Atualiza uma Tag com base nos dados fornecidos.")]
    public async Task<IActionResult> UpdateTag([FromBody] CommandTagDto request, int id)
    {
        var response = await tagService.UpdateTag(request, id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => Ok(response),
            ResponseTypeEnum.NotFound => NotFound(response),
            ResponseTypeEnum.InvalidInput => BadRequest(response.ValidationErrors),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Remover uma Tag", Description = "Remove uma Tag com base no ID.")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        var response = await tagService.DeleteTagById(id);

        return response.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(response.Type),
            _ => StatusCode(StatusCodes.Status500InternalServerError, response)
        };
    }
}