using MongoDB.Bson;
using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.Abstractions;

public interface IMongoDataAccess<TEntity> where TEntity : class, IAggregateRoot, new()
{
    public Task<TEntity> FindByIdAsync(Guid id);

    public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);

    public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector);

    public Task AddAsync(TEntity value);

    public Task AddRangeAsync(IEnumerable<TEntity> values);

    public Task<bool> UpdateAsync(TEntity value);

    public Task RemoveAsync(Guid id);

    public Task RemoveAsync(TEntity value);
}
