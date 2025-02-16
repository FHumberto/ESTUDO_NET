namespace T_Tier.BLL.DTOs.Users;

public class LoginResponseDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}