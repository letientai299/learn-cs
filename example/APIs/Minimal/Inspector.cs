using System.Diagnostics;
using Microsoft.AspNetCore.Routing.Template;

namespace Example.APIs.Minimal;

public class Inspector(
    IServiceCollection services,
    IServiceProvider prov,
    ICollection<TemplateMatcher> ss
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken) =>
        InspectAsync();

    public Task StopAsync(CancellationToken cancellationToken) =>
        InspectAsync();

    private Task InspectAsync()
    {
        var f = new StackFrame(1);
        Header($"{f.GetMethod()?.ReflectedType?.FullName}");

        var routingTypes =
            from s in services
            where s.ServiceType.Namespace == typeof(Route).Namespace
            select s;
        Log(routingTypes);

        Header("Endpoint data sources");
        var ds = prov.GetServices<EndpointDataSource>();
        Log(ds);
        foreach (var d in ds)
        {
            if (d is CompositeEndpointDataSource c)
            {
                Log(c.Endpoints);
            }
        }

        Header("Others");
        Log(ss);

        return Task.CompletedTask;
    }
}
