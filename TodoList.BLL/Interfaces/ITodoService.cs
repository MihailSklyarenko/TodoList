using TodoList.Shared.Models.Todo;

namespace TodoList.BLL.Interfaces;

public interface ITodoService
{
    Task<List<TodoViewModel>> GetAll(CancellationToken cancellationToken = default);
    Task<TodoViewModel> Add(TodoCreateModel createModel, CancellationToken cancellationToken = default);
    Task<TodoViewModel> Update(Guid id, TodoUpdateModel updateModel, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}
