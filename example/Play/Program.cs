UseDisposeAsDefer();
return;

static void UseDisposeAsDefer()
{
    using var a = new A();
    using var b = new B();
    Log(a, b);
}

#pragma warning disable SA1649, S3881
internal class A : IDisposable
{
    public void Dispose() => WriteLine("Dispose in A");
}

internal class B : IDisposable
{
    public void Dispose() => WriteLine("Dispose in B");
}
#pragma warning restore SA1649, S3881
