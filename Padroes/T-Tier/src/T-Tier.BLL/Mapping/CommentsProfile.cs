using AutoMapper;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.DAL.Entities;

namespace T_Tier.BLL.Mapping;

public class CommentsProfile : Profile
{
    public CommentsProfile()
    {
        CreateMap<Comment, QueryCommentResponseDto>();
        CreateMap<Comment, QueryCommentPostDto>();
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<UpdateCommentDto, Comment>();
    }
}