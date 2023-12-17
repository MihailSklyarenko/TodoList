using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TodoList.DataAccess;
using TodoList.DataAccess.Interfaces;
using TodoList.DataAccess.Repositories;

namespace TodoList.BLL.DependencyInjection;

public static class Database
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbContext, DatabaseContext>();

        services.AddDbContext<DatabaseContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            options.UseNpgsql(connectionString);
        });

        using (var provider = services.BuildServiceProvider())
        {
            var context = provider.GetRequiredService<DatabaseContext>();
            var migrations = context.Database.GetPendingMigrations();
            if (migrations.Any())
            {
                context.Database.Migrate();
            }
        }

        services.AddScoped<ITodoRepository, TodoRepository>();
    }
}
