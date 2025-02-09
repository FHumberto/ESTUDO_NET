using Microsoft.AspNetCore.Mvc;
using T_Tier.API.Wrappers;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.BLL.Services;

namespace T_Tier.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TagsController(TagService tagService) : ControllerBase
{
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
}
