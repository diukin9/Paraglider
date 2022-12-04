using MongoDB.Driver;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.MongoDB;

public interface IMongoDataAccess<TEntity> where TEntity : class, new()
{
    Task<List<object>> FindAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? sort = null,
        SortDirection sortDirection = SortDirection.Ascending,
        int skip = 0,
        int limit = 1000);

    Task<object?> FindByIdAsync(Guid id);

    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector);

    Task<long> CountAsync(Expression<Func<TEntity, bool>>? filter = null);

    Task AddAsync(TEntity value);

    void AddRange(IEnumerable<TEntity> values);

    Task AddRangeAsync(IEnumerable<TEntity> values);

    Task<bool> UpdateAsync(TEntity value);

    Task RemoveAsync(Guid id);

    Task RemoveAsync(TEntity entity);

    Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
}
