using System.Diagnostics;

namespace Example;

internal static class Program
{
  private static void Main()
  {
    var ps =
      from p in Process.GetProcesses(".")
      orderby p.Id descending
      select p;
    foreach (var p in ps)
    {
      Console.WriteLine($"{p.Id, 5}: {p.ProcessName}");
    }
  }
}
