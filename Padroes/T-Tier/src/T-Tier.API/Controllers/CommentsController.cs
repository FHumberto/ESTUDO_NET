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
     [SwaggerOperation(Summary = "Obter todos os Comentários",
         Description = "Retorna uma lista com todos os Comentários cadastrados.")]
     public async Task<IActionResult> GetAllPosts()
     {
         Response<IReadOnlyList<QueryCommentDto>> query = await commentService.GetAllCommentAsync();

         return query.Type switch
         {
             ResponseTypeEnum.Success => Ok(query.Result),
             ResponseTypeEnum.NotFound => NotFound(),
             _ => BadRequest()
         };
     }
}