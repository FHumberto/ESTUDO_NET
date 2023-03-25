using Postgrest.Attributes;
using Postgrest.Models;

namespace SupabaseNews.Models;

// nome da tabela no banco
[Table("newsletter")]
public class Newsletter : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("readtime")]
    public int ReadTime { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
