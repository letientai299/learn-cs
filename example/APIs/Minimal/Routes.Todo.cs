namespace Example.APIs.Minimal;

internal static partial class Routes
{
    internal static void Todos(WebApplication app)
    {
        var group = app.MapGroup("/todos");

        group.MapGet("/", (TodoDb db) => db.Todos);
        group.MapGet("/done", (TodoDb db) => db.Todos.Where(t => t.Done));
        group.MapPost(
            "/",
            (Todo todo, TodoDb db) =>
            {
                db.Todos.Add(todo);
                db.SaveChanges();
                return Results.Created($"/{todo.Id}", todo);
            }
        );

        TodoOne(group);
    }

    private static void TodoOne(RouteGroupBuilder group)
    {
        var one = group.MapGroup("/{id:int}");
        one.MapGet(
            "/",
            (int id, TodoDb db) =>
                db.Todos.Find(id) is { } todo
                    ? Results.Ok(todo)
                    : Results.NotFound()
        );

        one.MapPut(
            "/",
            (int id, Todo input, TodoDb db) =>
            {
                var todo = db.Todos.Find(id);
                if (todo is null)
                {
                    return Results.NotFound();
                }

                todo.Name = input.Name;
                todo.Done = input.Done;
                db.SaveChanges();
                return Results.NoContent();
            }
        );

        one.MapDelete(
            "/",
            (int id, TodoDb db) =>
            {
                if (db.Todos.Find(id) is not { } todo)
                {
                    return Results.NotFound();
                }

                db.Todos.Remove(todo);
                db.SaveChanges();
                return Results.NoContent();
            }
        );
    }
}
