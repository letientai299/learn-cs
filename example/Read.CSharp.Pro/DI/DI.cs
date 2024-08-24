using Example.DI;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
        services
            .AddHostedService<AppLifeTime>()
            .AddHostedService<Worker>()
            .AddSingleton<IWriter, LogWriter>()
    )
    .Build()
    .RunAsync();
