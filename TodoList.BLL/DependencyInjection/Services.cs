using Microsoft.Extensions.DependencyInjection;

using TodoList.BLL.Interfaces;
using TodoList.BLL.Services;

namespace TodoList.BLL.DependencyInjection;

public static class Services
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
    }
}