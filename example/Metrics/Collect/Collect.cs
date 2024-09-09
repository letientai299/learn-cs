using System.Diagnostics.Metrics;
using OpenTelemetry;
using OpenTelemetry.Metrics;

namespace Example.Metrics.Collect;

internal static class Collect
{
    // Running this will start an HTTP server, then, query :8080/metrics
    // to see the defined metrics.
    private static void Main()
    {
        using var meterProvider = Sdk.CreateMeterProviderBuilder()
            .AddMeter(Hats.Meter.Name)
            .AddPrometheusHttpListener(ops =>
                ops.UriPrefixes = ["http://localhost:8080/"]
            )
            .Build();

        var rand = Random.Shared;
        var hats = new Hats();

        WriteLine("Press any key to exit");
        while (!KeyAvailable)
        {
            // Simulate hat selling transactions.
            Thread.Sleep(rand.Next(100, 2500));
            hats.Sold.Add(rand.Next(0, 10));
        }
    }

    private sealed class Hats
    {
        internal Counter<int> Sold { get; } =
            Meter.CreateCounter<int>(
                "sold",
                "hats",
                "The number of hats sold in our store"
            );

        internal static readonly Meter Meter = new("has_com.hats", "1.0.0");
    }
}
