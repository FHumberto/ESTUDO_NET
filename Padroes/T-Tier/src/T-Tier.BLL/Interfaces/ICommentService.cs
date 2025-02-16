using T_Tier.BLL.DTOs.Comments;
using T_Tier.BLL.Wrappers;

namespace T_Tier.BLL.Interfaces;
public interface ICommentService
{
    Task<Response<IReadOnlyList<QueryCommentResponseDto>>> GetAllComment();
    Task<Response<QueryCommentResponseDto?>> GetCommentById(int id);
    Task<Response<int>> CreateComment(CreateCommentDto request);
    Task<Response<bool>> UpdateComment(UpdateCommentDto request, int id);
    Task<Response<bool>> SoftDeleteComment(int id);
    Task<Response<bool>> DeleteComment(int id);
}
