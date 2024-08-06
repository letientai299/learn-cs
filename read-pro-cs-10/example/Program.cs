namespace Main;

internal static class Program
{
  private static void Main()
  {
    var c = new Car(100);
    Log(c);
    c.Speed = 50;
    Log(c);
  }

  record Car(int MaxSpeed)
  {
    public int Speed
    {
      get => speed;
      set => speed = value < MaxSpeed ? value : MaxSpeed;
    }
    private int speed;
  }
}
