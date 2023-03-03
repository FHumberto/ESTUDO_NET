using TDLS.Data;

namespace TDLS.EndPoints.TodoItem;

public static class TodoItemGetAll
{
    public static string Template => "/todoitem"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action;

    public static IResult Action(TdlsContext context)
    {
        var todoItems = context.TodoItems.ToList();

        // FAZ COM QUE A RESPOSTA SÓ EXIBA OS DADOS NECESSÁRIOS
        var response = todoItems
            .Select(t => new TodoItemResponse { Id = t.Id, Tittle = t.Tittle, Description = t.Description, IsCompleted = t.IsCompleted, CreatedOn = t.CreatedOn, UpdatedOn = t.CreatedOn });

        return Results.Ok(response);
    }
}
