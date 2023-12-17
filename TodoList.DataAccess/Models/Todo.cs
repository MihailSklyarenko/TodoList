using TodoList.DataAccess.Models.Base;

namespace TodoList.DataAccess.Models;

public class Todo : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
