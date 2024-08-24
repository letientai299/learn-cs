using Example.APIs.Minimal;
using Microsoft.AspNetCore.HttpLogging;
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
    // Without AddEndpointsApiExplorer, swagger gen won't work.
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.All;
    })
    .AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseExceptionHandler("/errors");
app.UseStaticFiles();
app.UseDirectoryBrowser();
app.UseHealthChecks("/sys/health");

// https://rimdev.io/asp-net-core-routes-middleware
app.UseRouting();

Routes.Todos(app);
Routes.Debug(app);
Routes.NotFound(app);

var middlewares = app.GetMiddlewares();
Log(middlewares);

#pragma warning disable S6966
app.Run();

Header("done");
