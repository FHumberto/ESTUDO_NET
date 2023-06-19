using System.ComponentModel.DataAnnotations;

namespace CodingWiki_Model.Models;
public class SubCategory
{
    [Key]
    public int SubCategory_Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; }
}
