using Microsoft.EntityFrameworkCore;

using TodoList.DataAccess.Helpers.Enums;
using TodoList.DataAccess.Interfaces;
using TodoList.DataAccess.Models;

namespace TodoList.DataAccess.Repositories;

public class TodoRepository : Repository<Todo, Guid>, ITodoRepository
{
    public TodoRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Todo>> GetAll(CancellationToken cancellationToken = default)
    {
        return await GetBaseQuery(TrackingMode.NoTracking)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Todo> Add(Todo todo, CancellationToken cancellationToken = default)
    {
        await AddAsync(todo, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return todo;
    }

    public async Task<Todo> Update(Todo todo, CancellationToken cancellationToken = default)
    {
        base.Update(todo);
        await SaveChangesAsync(cancellationToken);
        return todo;
    }

    public async Task Delete(Todo todo, CancellationToken cancellationToken = default)
    {
        PhysicalDelete(todo);
        await SaveChangesAsync(cancellationToken);
    }
}
