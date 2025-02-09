namespace T_Tier.BLL.DTOs.Posts;

public class QueryPostDto
{
    public int Id { get; init; }
    public string? Tittle { get; init; }
    public string? Body { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
    public ICollection<string>? Tags { get; init; }
}