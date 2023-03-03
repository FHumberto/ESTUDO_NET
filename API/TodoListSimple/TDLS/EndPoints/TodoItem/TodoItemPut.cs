using Microsoft.AspNetCore.Mvc;

using TDLS.Data;

namespace TDLS.EndPoints.TodoItem;

public static class TodoItemPut
{
    public static string Template => "/todoitem/{id}"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] int id, TodoItemRequest todoItemRequest, bool isCompleted, TdlsContext context)
    {
        var todoItem = context.TodoItems.Where(x => x.Id == id).FirstOrDefault();

        if (todoItem == null)
            return Results.NotFound();

        todoItem.EditInfo(todoItemRequest.Tittle, todoItemRequest.Description, isCompleted);

        if (!todoItem.IsValid)
            return Results.ValidationProblem(todoItem.Notifications.ConvertToProblemDetails());

        context.SaveChanges();
        return Results.Ok();
    }
}
