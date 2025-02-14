namespace T_Tier.BLL.DTOs.Auth;

public class LoginRequestDto(string email, string password)
{
    public string Email { get; init; } = email;
    public string Password { get; init; } = password;
}