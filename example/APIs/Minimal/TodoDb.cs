using Microsoft.EntityFrameworkCore;

namespace Example.APIs.Minimal;

public class TodoDb(DbContextOptions<TodoDb> ops) : DbContext(ops)
{
    public DbSet<Todo> Todos => Set<Todo>();
}
