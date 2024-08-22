using System.Reflection;

namespace Example.APIs.Minimal;

public static class Ext
{
#pragma warning disable S3011
    /// <summary>
    /// Get all the registered middlewares using reflection.
    /// Very unsafe, as this relies on private implementation details.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <returns>Names of the middlewares.</returns>
    public static List<string> GetMiddlewares(this IApplicationBuilder app)
    {
        var builder = app.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
            .SingleOrDefault(p => p?.Name == "ApplicationBuilder", null)
            ?.GetValue(app);

        if (builder is not ApplicationBuilder b)
        {
            return [];
        }

        var fs = b.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        var components = fs.SingleOrDefault(p => p?.Name == "_components", null)
            ?.GetValue(b);

        if (components is not List<Func<RequestDelegate, RequestDelegate>> cs)
        {
            return [];
        }

        return cs.Select(c => c.Target?.ToString())
            .Where(s => s != null)
            .ToList()!;
    }
#pragma warning restore S3011
}
