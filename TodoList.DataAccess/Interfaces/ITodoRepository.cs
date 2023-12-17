using TodoList.DataAccess.Models;

namespace TodoList.DataAccess.Interfaces;

public interface ITodoRepository : IRepository<Todo, Guid>
{
    Task<IEnumerable<Todo>> GetAll(CancellationToken cancellationToken = default);
    Task<Todo> Add(Todo todo, CancellationToken cancellationToken = default);
    Task<Todo> Update(Todo todo, CancellationToken cancellationToken = default);
    Task Delete(Todo todo, CancellationToken cancellationToken = default);
}
