using EasyNetQ;

using Microsoft.Extensions.DependencyInjection;

using TodoList.BLL.Configuration;
using TodoList.BLL.HostedServices;

namespace TodoList.BLL.DependencyInjection;

public static class RabbitMQ
{
    public static void AddRabitBus(this IServiceCollection services, RabbitMQConfiguration options)
    {
        services.RegisterEasyNetQ(options.GetConnectionString(),
            x =>
            {
                x.EnableConsoleLogger();
                x.EnableSystemTextJson();
            });

        services.AddHostedService<QueueListener>();
    }
}