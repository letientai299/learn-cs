namespace Example.DI;

public class AppLifeTime(
    IHostApplicationLifetime hostAppLife,
    IConfiguration cfg
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        hostAppLife.ApplicationStarted.Register(
            () => Header($"App started at {DateTime.Now}")
        );
        hostAppLife.ApplicationStopping.Register(
            () => Header($"App stopping at {DateTime.Now}")
        );

        hostAppLife.ApplicationStopped.Register(() =>
        {
            Header($"App stopped at {DateTime.Now}");
            Show(cfg);
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;

    private static void Show(IConfiguration cfg)
    {
        foreach (var c in cfg.GetChildren())
        {
            if (c.Key != string.Empty)
            {
                WriteLine($"{c.Key} => {c.Value}");
            }
            Show(c);
        }
    }
}
