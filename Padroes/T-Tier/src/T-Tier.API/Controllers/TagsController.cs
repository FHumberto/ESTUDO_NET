using Microsoft.AspNetCore.Mvc;
using T_Tier.API.Wrappers;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.BLL.Services;

namespace T_Tier.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TagsController(TagService tagService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTags()
    {
        Response<IReadOnlyList<QueryTagDto>> query = await tagService.GetAllAsync();
        
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
    public async Task<IActionResult> GetTag(int id)
    {
        Response<QueryTagDto?> query = await tagService.GetByIdAsync(id);

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
    public async Task<IActionResult> CreateTag([FromBody] CommandTagDto request)
    {
        Response<int> command = await tagService.CreateAsync(request);

        return command.Type switch
        {
            ResponseTypeEnum.Success => CreatedAtAction(nameof(GetTag), new { id = command.Result }, new { id = command.Result }),
            _ => BadRequest(command.Errors)
        };
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTag([FromBody] CommandTagDto request, int id)
    {
        Response<bool> command = await tagService.UpdateAsync(request, id);
        
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
    public async Task<IActionResult> DeleteTag(int id)
    {
        Response<bool> deleteTask = await tagService.DeleteAsync(id);
        
        return deleteTask.Type switch
        {
            ResponseTypeEnum.Success => NoContent(),
            ResponseTypeEnum.NotFound => NotFound(),
            _ => BadRequest()
        };
    }
}
