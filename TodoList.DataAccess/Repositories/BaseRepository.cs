using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using TodoList.DataAccess.Helpers.Enums;
using TodoList.DataAccess.Interfaces;

namespace TodoList.DataAccess.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    private readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
    }

    public Task AddAsync(IEnumerable<TEntity> entities, CancellationToken token = default) =>
        GetDbSet(TrackingMode.TrackAll).AddRangeAsync(entities, token);

    public void Update(TEntity entity) => GetDbSet(TrackingMode.TrackAll).Update(entity);

    public void Update(IEnumerable<TEntity> entities) => GetDbSet(TrackingMode.TrackAll).UpdateRange(entities);

    public void PhysicalDelete(TEntity entity) => GetDbSet(TrackingMode.TrackAll).Remove(entity);

    public void PhysicalDelete(IEnumerable<TEntity> entities) => GetDbSet(TrackingMode.TrackAll).RemoveRange(entities);

    public virtual Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return _context.SaveChangesAsync(token);
    }

    protected DbSet<TEntity> GetDbSet(TrackingMode trackingMode = TrackingMode.NoTracking)
    {
        _context.ChangeTracker.QueryTrackingBehavior = (trackingMode == TrackingMode.TrackAll)
            ? QueryTrackingBehavior.TrackAll
            : QueryTrackingBehavior.NoTracking;

        return _context.Set<TEntity>();
    }

    protected IQueryable<TEntity> GetBaseQuery(TrackingMode trackingMode = TrackingMode.NoTracking) =>
        GetDbSet(trackingMode);

    protected IQueryable<TEntity> GetBaseQuery(TrackingMode trackingMode = TrackingMode.NoTracking,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetBaseQuery(trackingMode);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    public void Dispose() => _context.Dispose();
}