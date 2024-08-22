using Example.Play;
using Microsoft.Extensions.DependencyInjection;

Demo.UseDisposeAsDefer();

var services = new ServiceCollection();
services.AddSingleton<IServiceCollection>(services);
services.AddSingleton<Inspector>();

var provider = services.BuildServiceProvider();
var inspector = provider.GetService<Inspector>();
inspector?.Inspect();
