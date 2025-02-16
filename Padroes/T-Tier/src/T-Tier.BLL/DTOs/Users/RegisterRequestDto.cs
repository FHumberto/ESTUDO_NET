namespace T_Tier.BLL.DTOs.Users;

public class RegisterRequestDto(string firstName, string lastName, string email, string userName, string password)
{
    public required string FirstName { get; init; } = firstName;
    public required string LastName { get; init; } = lastName;
    public required string Email { get; init; } = email;
    public required string UserName { get; init; } = userName;
    public required string Password { get; init; } = password;
}