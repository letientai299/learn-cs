namespace Main;

internal static class Program
{
  private static void Main()
  {
    var c = new Car(100);
    Log(c);
    c.Speed = 51;
    Log(c);

    I1? a = new A();
    a.Hi();
    a = c as I1;
    a.Hi();

    Console.WriteLine("--------");
    var b = new A();
    b.How();
  }

  interface I1
  {
    void Hi() => Console.WriteLine($"hi from I1: {GetType()}");
  }

  interface I2
  {
    void How();
  }

  class A : I1, I2
  {
    public void How() => Console.WriteLine($"howdy: {GetType()}");
  }

  record Car(int MaxSpeed) : I1
  {
    public int Speed
    {
      get => speed;
      set => speed = value < MaxSpeed ? value : MaxSpeed;
    }
    private int speed;
  }
}
