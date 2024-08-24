var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

// NOTE: without this, controller paths won't be recognized.
app.MapControllers();

app.MapPost("/", (HttpContext _) => Results.NoContent());

await app.RunAsync();
