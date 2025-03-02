namespace ScalarApiLabs.Models.Dto.Account;

public class LoginRequestDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}