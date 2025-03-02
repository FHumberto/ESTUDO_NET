using ScalarApiLabs.Models.Dto.Account;

namespace ScalarApiLabs.Interfaces.Services;

public interface IUserService
{
    public string UserId { get; }
    Task<LoginResponseDto> Login(LoginRequestDto requestDto);
    Task<RegisterResponseDto> Register(RegisterRequestDto requestDto);
}
