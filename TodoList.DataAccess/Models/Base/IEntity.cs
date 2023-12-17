namespace TodoList.DataAccess.Models.Base;

public interface IEntity<TId> where TId : struct
{
    public TId Id { get; set; }
}
