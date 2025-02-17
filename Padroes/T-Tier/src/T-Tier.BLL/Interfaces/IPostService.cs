using T_Tier.BLL.DTOs.Posts;
using T_Tier.BLL.Wrappers;

namespace T_Tier.BLL.Interfaces;
public interface IPostService
{
    Task<Response<IReadOnlyList<QueryPostResponseDto>>> GetAllPost();
    Task<Response<QueryPostResponseDto?>> GetPostById(int id);
    Task<Response<QueryPostTagResponseDto?>> GetPostByIdWithTag(int id);
    Task<Response<QueryPostCommentsResponseDto?>> GetPostByIdWithComments(int id);
    Task<Response<int>> CreatePost(CommandPostRequestDto request);
    Task<Response<bool>> UpdatePost(CommandPostRequestDto request, int id);
    Task<Response<bool>> DeletePostById(int id);
    Task<Response<bool>> SoftDeletePostById(int id);
}
