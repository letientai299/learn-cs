namespace Example.APIs.Minimal;

internal static class Routes
{
    internal static void NotFound(WebApplication app) =>
        app.Map("/{**notFound}", () => Results.BadRequest("no such thing"));

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

    internal static void Debug(WebApplication app)
    {
        var group = app.MapGroup("/debug");
        group.MapGet("/", Whatever);
        group.MapGet("/hi", (HttpContext _) => Results.Ok("Hello"));
        group.MapGet("/err", _ => throw new ArgumentException("some"));
        group.MapGet("/routes", ListRoutes);
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

    private static IResult Whatever(TodoDb db) =>
        Results.Ok($"DB name: {db.Database.ProviderName}");

    private static IResult ListRoutes(
        IEnumerable<EndpointDataSource> ds,
        IWebHostEnvironment env,
        ILogger<Todo> lg
    )
    {
        var data =
            from d in ds.ToList()
            from path in d.Endpoints
            select path.DisplayName;

        lg.LogInformation("{Data}", data);

        return Results.Ok(new { endpoints = data, env });
    }
}
