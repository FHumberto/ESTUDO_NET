namespace T_Tier.BLL.DTOs.Comments;

public class CreateCommentDto
{
    public int PostId { get; set; }
    public string? Body { get; init; }
}