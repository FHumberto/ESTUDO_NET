namespace T_Tier.BLL.DTOs.Users;

public class AuthRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}