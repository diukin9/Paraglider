using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common.Repository;

public interface IRepository<TEntity> where TEntity : class, new()
{
    public Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken cancellationToken);

    public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector);

    public Task<TEntity?> FindByIdAsync(Guid id);

    public Task AddAsync(TEntity entity);

    public Task AddRangeAsync(IEnumerable<TEntity> entities);

    public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector);

    public Task RemoveAsync(Guid id);

    public Task RemoveAsync(TEntity entity);

    public Task RemoveRangeAsync(IEnumerable<TEntity> entities);

    public Task UpdateAsync(TEntity entity);

    public Task SaveChangesAsync();
}
