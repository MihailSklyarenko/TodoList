using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using TodoList.DataAccess.Models;
using TodoList.Shared.Models.Todo;

namespace TodoList.BLL.DependencyInjection;

public static class Mapper
{
    public static void AddMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile(new AutoMapperProfile());
        });

        services.AddSingleton(mapperConfig.CreateMapper());
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, TodoViewModel>();
            CreateMap<TodoCreateModel, Todo>();
            CreateMap<TodoUpdateModel, Todo>();
        }
    }
}