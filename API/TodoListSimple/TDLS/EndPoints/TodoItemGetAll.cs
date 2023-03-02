using TDLS.Data;

namespace TDLS.EndPoints;

public static class TodoItemGetAll
{
    public static string Template => "/todo"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action;

    public static IResult Action(TdlsContext context)
    {
        var response = context.TodoItems.ToList();
        return Results.Ok(response);
    }
}
