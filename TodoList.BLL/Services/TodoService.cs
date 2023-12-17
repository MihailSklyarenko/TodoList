using AutoMapper;

using EasyNetQ;

using TodoList.BLL.Exceptions;
using TodoList.BLL.Interfaces;
using TodoList.DataAccess.Interfaces;
using TodoList.DataAccess.Models;
using TodoList.Shared.Models.Todo;
using TodoList.Shared.Models.Todo.QueueEvents;

namespace TodoList.BLL.Services;

internal class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public TodoService(ITodoRepository todoRepository, IMapper mapper, IBus bus)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<List<TodoViewModel>> GetAll(CancellationToken cancellationToken = default)
    {
        var todos = await _todoRepository.GetAll(cancellationToken);
        var result = _mapper.Map<List<TodoViewModel>>(todos);
        return result;
    }

    public async Task<TodoViewModel> Add(TodoCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var todo = _mapper.Map<Todo>(createModel);
        var newTodo = await _todoRepository.Add(todo, cancellationToken);
        await SendToQueue(newTodo.Id, TodoState.Added, cancellationToken);
        return _mapper.Map<TodoViewModel>(newTodo);
    }

    public async Task<TodoViewModel> Update(Guid id, TodoUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var todo = await _todoRepository.GetByIdAsync(id) ?? throw new BadRequestException("Todo not found");
        _mapper.Map(updateModel, todo);
        await _todoRepository.Update(todo, cancellationToken);
        await SendToQueue(todo.Id, TodoState.Updated, cancellationToken);

        return _mapper.Map<TodoViewModel>(todo);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await _todoRepository.GetByIdAsync(id) ?? throw new BadRequestException("Todo not found");

        await _todoRepository.Delete(todo, cancellationToken);
        await SendToQueue(todo.Id, TodoState.Deleted, cancellationToken);
    }

    private async Task SendToQueue(Guid id, TodoState todoState, CancellationToken cancellationToken)
    {
        await _bus.PubSub.PublishAsync(
            new TodoMessage
            {
                Id = id,
                State = todoState,
            },
            x => x.WithTopic("Todo"),
            cancellationToken);
    }
}
