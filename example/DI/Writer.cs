using System.Diagnostics;

namespace Example.DI;

internal class Writer : IWriter
{
    public Writer()
    {
        var writerCreatedAt = new StackTrace(2);
        Log(writerCreatedAt);
    }

    public void Write(string msg) => WriteLine($"Writer: {msg}");
}
