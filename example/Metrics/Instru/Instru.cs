using System.Diagnostics.Metrics;

// https://learn.microsoft.com/en-us/dotnet/core/diagnostics/metrics-instrumentation
// dotnet tool install --global dotnet-counters

namespace Example.Metrics.Instru;

internal static class Instru
{
    private static int observed = 100;

    public static void Main()
    {
        var meter = new Meter("hats");
        var cnt = meter.CreateCounter<int>("sold");
        var rnd = new Random();

        var observable = meter.CreateObservableCounter(
            "sold.observed",
            Observe
        );

        var lis = new MeterListener();
        lis.EnableMeasurementEvents(cnt);
        lis.EnableMeasurementEvents(observable);
        lis.SetMeasurementEventCallback<int>(ShowValues);

        Loop(rnd, cnt);

        Header("Before exit");
        lis.RecordObservableInstruments();
    }

    private static int Observe()
    {
        var observedValue = Volatile.Read(ref observed);
        Log(observedValue);
        return observedValue;
    }

    private static void Loop(Random rnd, Counter<int> cnt)
    {
        while (!KeyAvailable)
        {
            var duration = TimeSpan.FromMilliseconds(
                ((rnd.Next() % 10) + 1) * 100
            );

            Thread.Sleep(duration);
            cnt.Add(1);
            observed += rnd.Next(4);
        }
    }

    private static void ShowValues(
        Instrument ins,
        // Note: this value is what get pass to `Add` for the Counter,
        // and what is returned for the ObservableCounter.
        int value,
        ReadOnlySpan<KeyValuePair<string, object?>> tags,
        object? state
    )
    {
        var desc = string.Join(
            ", ",
            tags.ToArray().Select(kv => $"{kv.Key}={kv.Value}")
        );

        WriteLine($"{ins.Name}: {value}, tags=[{desc}], state=[{state}]");
    }
}
