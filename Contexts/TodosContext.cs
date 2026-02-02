using Microsoft.EntityFrameworkCore;

namespace ASPnetAPIrest.Models;

public class TodosContext : DbContext{
    public DbSet<Todo> Todos => Set<Todo>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=todos.sqlite3");
    }
}