namespace Example.DI;

internal class Worker(IWriter w) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            w.Write($"Worker is running at {DateTimeOffset.Now}");
            await Task.Delay(1_000, token);
        }
    }
}
