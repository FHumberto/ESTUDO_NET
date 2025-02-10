#region

using AutoMapper;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.DAL.Entities;

#endregion

namespace T_Tier.BLL.Mapping;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        //? converte de [A ==> B]
        CreateMap<Post, QueryPostDto>();
        CreateMap<Post, QueryPostTagDto>()
            .ForMember(dest => dest.Tags, opt
                => opt.MapFrom(src => src.Tags!.Select(tag => tag.Name).ToList()));
        CreateMap<CommandPostDto, Post>();
    }
}