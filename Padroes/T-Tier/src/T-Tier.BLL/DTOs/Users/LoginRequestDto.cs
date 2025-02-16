namespace T_Tier.BLL.DTOs.Users;

public class LoginRequestDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}