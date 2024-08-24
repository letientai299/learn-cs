namespace Example.DI;

internal class LogWriter(ILogger<LogWriter> lg) : IWriter
{
    public void Write(string message) =>
        lg.LogInformation("msg {Msg}", message);
}
