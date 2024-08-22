using Microsoft.Extensions.DependencyInjection;

namespace Example.Play;

internal class Inspector(IServiceCollection services)
{
    public void Inspect()
    {
        foreach (var sv in services)
        {
            Log(sv);
        }
    }
}
