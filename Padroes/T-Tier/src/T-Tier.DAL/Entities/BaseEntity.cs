namespace T_Tier.DAL.Entities;

public class BaseEntity
{
    public int Id { get; init; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}