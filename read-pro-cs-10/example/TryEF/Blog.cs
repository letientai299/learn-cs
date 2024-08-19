using Example.CompiledModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record Blog
{
    public int Id { get; set; }

    public string Url { get; set; } = string.Empty;

    public List<Post> Posts { get; } = [];
}

public record Post
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public Blog Blog { get; init; } = new();
}

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Post> Posts { get; set; }

    private string DbPath { get; }

    public BloggingContext(LogLevel lvl = LogLevel.Information)
    {
        LogLvl = lvl;
        DbPath = Path.Join(TopBinDir(), "blogs.db");
        Log(DbPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder ops) =>
        ops.UseLoggerFactory(
                LoggerFactory.Create(b =>
                    b.AddSimpleConsole(o => o.TimestampFormat = "HH:mm:ss.fff ")
                        .SetMinimumLevel(LogLvl)
                )
            )
            .EnableSensitiveDataLogging()
            .EnableServiceProviderCaching()
            .EnableThreadSafetyChecks()
            .UseModel(BloggingContextModel.Instance)
            .UseSqlite($"Data Source={DbPath}");

    private LogLevel LogLvl { get; }
}
