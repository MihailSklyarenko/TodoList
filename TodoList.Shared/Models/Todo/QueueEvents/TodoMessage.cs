namespace TodoList.Shared.Models.Todo.QueueEvents;

public class TodoMessage
{
    public Guid Id { get; set; }
    public TodoState State { get; set; }
}

public enum TodoState
{
    Added,
    Updated,
    Deleted
}