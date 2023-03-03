using Microsoft.AspNetCore.Mvc;

using TDLS.Data;

namespace TDLS.EndPoints.TodoItem;

public static class TodoItemDelete
{
    public static string Template => "/todoitem/{id}"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] int id, TdlsContext context)
    {
        var todoItem = context.TodoItems.Where(x => x.Id == id).FirstOrDefault();

        if (todoItem == null)
            return Results.NotFound();

        context.TodoItems.Remove(todoItem);
        context.SaveChanges();

        return Results.Ok();
    }
}
