namespace Example.APIs.Minimal;

public record Todo
{
    public Todo() { }

    public Todo(int id, string? name, bool done)
    {
        Done = done;
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Done { get; set; }
}
