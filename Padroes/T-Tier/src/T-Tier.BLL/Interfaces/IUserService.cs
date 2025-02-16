using T_Tier.BLL.DTOs.Users;
using T_Tier.BLL.Wrappers;

namespace T_Tier.BLL.Interfaces;

public interface IUserService
{
    Task<Response<LoginResponseDto>> Login(LoginRequestDto requestDto);
    Task<Response<RegisterResponseDto>> Register(RegisterRequestDto requestDto);
    Task<Response<QueryUserResponseDto>> FindUserById(string userId);
    Task<Response<IReadOnlyList<QueryUserPostsResponseDto>>> FindPostsWithUser(string userId);
    Task<Response<IReadOnlyList<QueryUserCommentsResponseDto>>> FindCommentsWithUser(string userId);
    Task<Response<IReadOnlyList<QueryUserRoleResponseDto>>> GetAllRoles();
    Task<Response<bool>> DeleteUser(string userId);
    Task<Response<bool>> SoftDeleteUser(string userId);
}