using System.ComponentModel.DataAnnotations;

namespace TDLS.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Tittle { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime EditedOn { get; set; }
}
