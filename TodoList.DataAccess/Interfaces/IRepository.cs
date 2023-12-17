using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Query;

using TodoList.DataAccess.Helpers.Enums;

namespace TodoList.DataAccess.Interfaces;

public interface IRepository<TEntity, TKey> : IBaseRepository<TEntity>
    where TEntity : class
    where TKey : struct
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default);

    Task<TEntity?> GetByIdAsync(TKey id,
        CancellationToken token = default,
        TrackingMode tracking = TrackingMode.NoTracking);

    Task<TEntity?> SingleOrDefaultAsync(TrackingMode tracking = TrackingMode.NoTracking,
        CancellationToken token = default,
        params Expression<Func<TEntity, object>>[] includes);
}