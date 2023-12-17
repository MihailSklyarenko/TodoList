namespace TodoList.DataAccess.Interfaces;

public interface IBaseRepository<TEntity> : IDisposable
    where TEntity : class
{
    Task AddAsync(IEnumerable<TEntity> entities, CancellationToken token = default);

    void Update(TEntity entity);

    void Update(IEnumerable<TEntity> entities);

    void PhysicalDelete(TEntity entity);

    void PhysicalDelete(IEnumerable<TEntity> entities);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}