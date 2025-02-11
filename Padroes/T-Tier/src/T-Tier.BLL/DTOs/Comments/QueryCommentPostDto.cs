namespace T_Tier.BLL.DTOs.Comments;

public class QueryCommentPostDto
{
    public int Id { get; init; }
    public int PostId { get; init; }
    public string? Body { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}