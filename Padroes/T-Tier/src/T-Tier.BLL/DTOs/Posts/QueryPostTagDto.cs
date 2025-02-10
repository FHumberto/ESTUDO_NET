namespace T_Tier.BLL.DTOs.Posts;

public class QueryPostTagDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Body { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public ICollection<string>? Tags { get; init; }
}