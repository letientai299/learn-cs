namespace Example.APIs.Minimal;

internal static partial class Routes
{
    internal static void Debug(WebApplication app)
    {
        var group = app.MapGroup("/debug");
        group.MapGet("/", Whatever);

        group.MapGet("/err", _ => throw new ArgumentException("some"));

        group.MapGet("/routes", ListRoutes);
    }

    private static IResult Whatever(TodoDb db) =>
        Results.Ok($"DB name: {db.Database.ProviderName}");

    // TODO (tai): why the below lines crash the DI?
    // private static IResult ListRoutes(List<EndpointDataSource> ds)
    // private static IResult ListRoutes(ICollection<EndpointDataSource> ds)
    // but this is ok?
    private static IResult ListRoutes(
        IEnumerable<EndpointDataSource> ds,
        IWebHostEnvironment env,
        ILogger<Todo> lg
    )
    {
        var data = (
            from d in ds.ToList()
            from path in d.Endpoints
            select path.DisplayName
        ).ToArray();

        lg.LogInformation(string.Join("\n", data));

        return Results.Ok(new { endpoints = data, env });
    }
}
