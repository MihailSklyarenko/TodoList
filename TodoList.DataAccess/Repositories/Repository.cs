using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using TodoList.DataAccess.Helpers.Enums;
using TodoList.DataAccess.Interfaces;
using TodoList.DataAccess.Models.Base;

namespace TodoList.DataAccess.Repositories;

public class Repository<TEntity, TKey> : BaseRepository<TEntity>, IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    public Repository(DbContext context) : base(context)
    { }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default)
    {
        var task = await GetDbSet(TrackingMode.TrackAll).AddAsync(entity, token);
        return task.Entity;
    }
    public Task<TEntity?> GetByIdAsync(TKey id,
    CancellationToken token = default,
    TrackingMode tracking = TrackingMode.NoTracking)
    {
        return GetBaseQuery(tracking)
            .SingleOrDefaultAsync(x => x.Id.Equals(id), token);
    }

    public Task<TEntity?> SingleOrDefaultAsync(TrackingMode tracking = TrackingMode.NoTracking,
        CancellationToken token = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return GetBaseQuery(tracking, includes)
            .SingleOrDefaultAsync(token);
    }
}