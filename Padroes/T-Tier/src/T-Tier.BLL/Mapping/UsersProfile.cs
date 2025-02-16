using AutoMapper;
using Microsoft.AspNetCore.Identity;
using T_Tier.BLL.DTOs.Users;
using T_Tier.DAL.Entities;

namespace T_Tier.BLL.Mapping;
internal class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, QueryUserResponseDto>()
            .ForMember(dest => dest.UserRole,
            opt => opt.Ignore());

        CreateMap<Post, QueryUserPostsResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<Comment, QueryUserCommentsResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<IdentityRole, QueryUserRoleResponseDto>();
    }
}
