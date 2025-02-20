using T_Tier.BLL.DTOs.Tags;
using T_Tier.BLL.Wrappers;

namespace T_Tier.BLL.Interfaces;
public interface ITagService
{
    Task<Response<IReadOnlyList<QueryTagResponseDto>>> GetAllTag();
    Task<Response<QueryTagResponseDto?>> GetTagById(int id);
    Task<Response<int>> CreateTag(CommandTagDto request);
    public Task<Response<bool>> AddTagToPost(CommandAddTagPostRequest request, int postId);
    Task<Response<bool>> UpdateTag(CommandTagDto request, int id);
    Task<Response<bool>> DeleteTagById(int id);
}
