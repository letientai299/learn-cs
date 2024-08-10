namespace Example;

internal static class Program
{
  private static void Main()
  {
    string[] games =
    {
      "Morrowind",
      "Uncharted 2",
      "Fallout 3",
      "Daxter",
      "System Shock 2"
    };

    var vs = from g in games where g.Contains(' ') select g;
    string[] arr = vs.ToArray();
    //foreach (var v in arr)
    //{
    //  Log(v);
    //}
    Log(arr);
    string[] empty = [];
    Log(empty);
    //Log(vs.ToArray());
  }
}
