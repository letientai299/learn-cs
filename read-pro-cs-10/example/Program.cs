namespace Example;

internal static class Program
{
  private static void Main()
  {
    Base10 d = new("123");
    Base2 b = new("01010101");
    Log(d, b);
    Base x = b;
    Log(x.ToString());
    Log(x.Val);

    d = b;
    Log(d);

    b = new Base10("1234");
    Log(b);
  }

  private sealed record Base2(string Data) : Base(2, Data)
  {
    public override string ToString() => base.ToString();

    public static implicit operator Base10(Base2 b) => new(b.Val.ToString());
  }

  private sealed record Base10(string Data) : Base(10, Data)
  {
    public override string ToString() => base.ToString();

    public static implicit operator Base2(Base10 v) =>
      new(Convert.ToString(v.Val, 2));
  }

  private abstract record Base(int From, string Data)
  {
    public int Val { get; } = Convert.ToInt32(Data, From);

    public override string ToString() => $"[base={From}, data={Data}] {Val}";
  }
}
