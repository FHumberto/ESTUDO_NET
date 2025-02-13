namespace T_Tier.BLL.DTOs.Users;

public class RegisterRequestDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
}