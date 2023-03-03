namespace TDLS.EndPoints.TodoItem;

public class TodoItemResponse
{
    public int Id { get; set; }
    public string? Tittle { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
