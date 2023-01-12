using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Infrastructure.Common.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot, new()
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _context
            .Set<TEntity>()
            .ToArrayAsync(cancellationToken);
    }

    public abstract Task<TEntity?> FindByIdAsync(Guid id);

    public abstract Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector);

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector)
    {
        return await _context.Set<TEntity>().AnyAsync(selector);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.FromResult(_context.Set<TEntity>().Remove(entity));
    }

    public async Task RemoveAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().SingleAsync(x => x.Id == id);
        await RemoveAsync(entity);
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        await Task.FromResult(default(TEntity));
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await Task.FromResult(_context.Set<TEntity>().Update(entity));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().Where(filter).CountAsync();
    }
}
