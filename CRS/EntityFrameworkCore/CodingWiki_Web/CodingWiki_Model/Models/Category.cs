using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingWiki_Model.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Column("Name")]
    [Required]
    public string CategoryName { get; set; }
    //public int DisplayOrder { get; set; }
}
