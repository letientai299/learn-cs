using Example.APIs.Minimal;
using Microsoft.AspNetCore.Routing.Template;
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
    // .AddSingleton(builder.Services)
    // .AddHostedService<Inspector>()
    // Some exploration about why was AddEndpointsApiExplorer added.
    // https://stackoverflow.com/a/71933300/3869533
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddHealthChecks();

var app = builder.Build();

// https://rimdev.io/asp-net-core-routes-middleware
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/errors");
app.UseStaticFiles();
app.UseDirectoryBrowser();
app.UseHealthChecks("/sys/health");

Routes.Todos(app);
Routes.Debug(app);

var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope();
var ss = scope.ServiceProvider.GetServices<TemplateBinder>();
Log(ss);
#pragma warning disable S6966
// app.Run();

var middlewares = app.GetMiddlewares();
Log(middlewares);
Header("done");


// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/webapplication?view=aspnetcore-8.0#access-the-dependency-injection-di-container
