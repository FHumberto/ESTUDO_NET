namespace T_Tier.BLL.DTOs.Posts;

public class QueryPostTagResponseDto
{
    public string? Title { get; init; }
    public string? Body { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public ICollection<object>? Tags { get; init; }
}