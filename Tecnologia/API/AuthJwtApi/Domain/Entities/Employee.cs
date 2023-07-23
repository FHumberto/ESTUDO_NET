namespace AuthJwtApi.Domain.Entities;

public class Employee : BaseEntity
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string Name { get; set; }
    public string? Role { get; set; }
}
