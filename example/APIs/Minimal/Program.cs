using Example.APIs.Minimal;
using Microsoft.EntityFrameworkCore;

var loggerFactory = LoggerFactory.Create(b =>
    b.SetMinimumLevel(LogLevel.Debug)
        .AddSimpleConsole(o => o.TimestampFormat = "HH:mm:ss.fff ")
);

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddDatabaseDeveloperPageExceptionFilter()
    .AddDbContext<TodoDb>(opt =>
        opt.UseInMemoryDatabase("Todos").UseLoggerFactory(loggerFactory)
    );

builder
    .Services
    // Some exploration about why was AddEndpointsApiExplorer added.
    // https://stackoverflow.com/a/71933300/3869533
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();
app.UseStaticFiles();

Routes.Todos(app);
Routes.Debug(app);

#pragma warning disable S6966
app.Run();
