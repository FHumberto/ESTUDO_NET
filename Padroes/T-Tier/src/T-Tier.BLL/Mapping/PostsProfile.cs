using AutoMapper;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.DAL.Entities;

namespace T_Tier.BLL.Mapping;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        //? converte de [A ==> B]
        CreateMap<Post, QueryPostDto>();
        
        CreateMap<Post, QueryPostTagDto>()
            .ForMember(dest => dest.Tags,
            opt => opt.MapFrom(src =>
                src.Tags!.Select(tag => new { Id = tag.Id, Name = tag.Name }).ToList()));
        
        CreateMap<Post, QueryPostCommentsDto>()
            .ForMember(dest => dest.Comments,
                opt => opt.MapFrom(src =>
                    src.Comments!.Select(comment => new { Id = comment.Id, Body = comment.Body }).ToList()));
        
        CreateMap<CommandPostDto, Post>();
    }
}