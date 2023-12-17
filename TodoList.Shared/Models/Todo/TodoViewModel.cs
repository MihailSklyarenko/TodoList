namespace TodoList.Shared.Models.Todo;

public class TodoViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}