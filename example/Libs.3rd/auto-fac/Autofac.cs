using Autofac;

var builder = new ContainerBuilder();
builder.RegisterType<DatedWriter>().Named<IWriter>("dated");

builder
    .RegisterType<ConsoleWriter>()
    .Named<IWriter>("console")
    // NOTE: using named disables the ability to resolve the interface
    // without any "key" (named is just keyed using string key).
    // The "As" line to also register the implementation as a bare interface.
    .As<IWriter>();

var container = builder.Build();

// this works, but isn't recommended by Autofac
// because lifetime scope is not managed.
Header("Resolving using container");
var writer = container.Resolve<IWriter>();
writer.Write("some");

Header("Resolving using scope");
using (var scope = container.BeginLifetimeScope())
{
    var w = scope.ResolveNamed<IWriter>("dated");
    w.Write("something");
}

Header("Done");

internal interface IWriter
{
    void Write(string msg);
}

internal class ConsoleWriter : IWriter
{
    public void Write(string msg) => WriteLine($"[Console] {msg}");
}

internal class DatedWriter : IWriter
{
    public void Write(string msg) =>
        WriteLine($"[{TimeOnly.FromDateTime(DateTime.Now)}] {msg}");
}
