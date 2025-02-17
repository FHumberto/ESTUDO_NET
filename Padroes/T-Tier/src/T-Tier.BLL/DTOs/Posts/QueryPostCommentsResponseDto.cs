namespace T_Tier.BLL.DTOs.Posts;

public class QueryPostCommentsResponseDto
{
    public string? Title { get; init; }
    public string? Body { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public ICollection<object>? Comments { get; init; }
}