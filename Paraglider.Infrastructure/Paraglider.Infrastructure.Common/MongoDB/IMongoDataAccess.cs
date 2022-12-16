using MongoDB.Driver;
using Paraglider.Infrastructure.Common.Interfaces;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.MongoDB;

public interface IMongoDataAccess<TEntity> where TEntity : class, IIdentified<string>, new()
{
    Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? sort = null,
        SortDirection sortDirection = SortDirection.Ascending,
        int skip = 0,
        int limit = 1000);

    Task<TEntity?> FindByIdAsync(string id);

    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector);

    Task<long> CountAsync(Expression<Func<TEntity, bool>>? filter = null);

    Task AddAsync(TEntity value);

    void AddRange(IEnumerable<TEntity> values);

    Task AddRangeAsync(IEnumerable<TEntity> values);

    Task<bool> UpdateAsync(TEntity value);

    Task<bool> PartialUpdateAsync<TField>(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TField>> update,
        TField value);

    Task RemoveAsync(string id);

    Task RemoveAsync(TEntity entity);

    Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
}
