namespace T_Tier.BLL.DTOs.Users;

public class QueryUserResponseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; init; }
    public string? EmailConfirmed { get; set; }
    public string? UserRole { get; set; }
    public string? IsDeleted { get; set; }
}
