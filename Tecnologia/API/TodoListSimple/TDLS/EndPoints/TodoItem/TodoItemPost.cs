using TDLS.Data;

namespace TDLS.EndPoints.TodoItem;

public static class TodoItemPost
{
    public static string Template => "/todoitem"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(TodoItemRequest todoItemRequest, TdlsContext context)
    {
        var todoItem = new Models.TodoItem(todoItemRequest.Tittle, todoItemRequest.Description);

        if (!todoItem.IsValid)
        {
            return Results.ValidationProblem(todoItem.Notifications.ConvertToProblemDetails());
        }

        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();

        return Results.Created($"/todoitem/{todoItem.Id}", todoItem.Id);
    }
}
