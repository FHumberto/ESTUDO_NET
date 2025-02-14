using T_Tier.BLL.DTOs.Auth;
using T_Tier.BLL.Wrappers;

namespace T_Tier.BLL.Interfaces;

public interface IAuthService
{
    Task<Response<LoginResponseDto>> Login(LoginRequestDto requestDto);
    Task<Response<RegisterResponseDto>> Register(RegisterRequestDto requestDto);
}