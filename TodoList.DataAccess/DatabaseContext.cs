using Microsoft.EntityFrameworkCore;

using TodoList.DataAccess.Models;

namespace TodoList.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
}