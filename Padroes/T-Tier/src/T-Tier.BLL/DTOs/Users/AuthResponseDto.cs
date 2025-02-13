namespace T_Tier.BLL.DTOs.Users;

public class AuthResponseDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}