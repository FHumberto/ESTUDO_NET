using System.ComponentModel.DataAnnotations.Schema;

namespace T_Tier.DAL.Entities;

public class BaseEntity
{
    //? força o entity framework a ir de 1 em 1
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}